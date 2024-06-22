using System.Security.Claims;
using AutoMapper;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Auth.Specification;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Auth.Services.AuthService
{
    public sealed class AuthService : IAuthService
    {
        #region -- Properties --
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Account> _repository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        #endregion -- Properties --

        #region -- Public Method --

        public AuthService(IMapper mapper, IRoleService roleService, IUnitOfWork unitOfWork, IImageService imageService)
        {
            _imageService = imageService;
            _mapper = mapper;
            _roleService = roleService;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GenericRepository<Account>();
        }

        public async Task<bool> Create(AuthRequest request, int userId)
        {
            Account account = _mapper.Map<Account>(request);
            Role role = await _roleService.getRole(request.RoleName);
            account.Password = PassEncrypt.hashPassword(request.Password);
            account.Role = role;
            account.Status = (int) EnumsApp.Waiting;
            if (account.Role.RoleName == AppConstants.COMPANY)
            {
                account.Status = (int)EnumsApp.Lock;
            }

            await _repository.Create(account, userId);
            return true;
        }

        public Task<bool> ChangeIsActive(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeIsLock(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeToWaiting(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeToDisable(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistByParam(string param)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAllByAdmin(object pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAll(object pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAll(object pagingRequest, int idMaster, bool checkStatus = false)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteHard(int id)
        {
            AccountSpecification accountSpecification = new AccountSpecification(id, checkStatus:false, isDelete:true);
            Account account = await _repository.Get(accountSpecification);
            return await _repository.DeleteHard(account);
        }

        public Task<List<AuthResponse>> GetAllByAdmin()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ResetPass(FormResetPass request, int userId)
        {

            AccountSpecification accountSpecification = new AccountSpecification(request.Username);
            Account account = await _repository.Get(accountSpecification) ?? throw new AuthException(AuthConstants.UNAUTHORIZATION);
            if (account.Customer != null)
            {
                if (account.Customer.Email != request.Email ||
                    account.Customer.PhoneNumber != request.PhoneNumber)
                    throw new AuthException(AuthConstants.UNAUTHORIZATION);
            }
            else 
            {
                if(account.Company.Email != request.Email ||
                   account.Company.PhoneNumber != request.PhoneNumber)
                    throw new AuthException(AuthConstants.UNAUTHORIZATION);
            }

            if (!PassEncrypt.VerifyPassword(request.PasswordOld, account.Password))
            {
                throw new AuthException(AuthConstants.UNAUTHORIZATION);
            }

            account.Password = PassEncrypt.hashPassword(request.PasswordNew);
            await _repository.Update(account, userId:userId);
            return true;
        }

        public async Task<AuthResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = JwtUtils.GetPrincipal(request.Token);
            var username = principal.FindFirstValue("Username");
            var account = await GetAccountByUsername(username); //retrieve the refresh token from a data store
            if (account.RefreshToken != request.RefreshToken)
                throw new AuthException(AuthConstants.REFRESH_TOKEN_FAIL);
            account.RefreshToken = JwtUtils.GenerateRefreshToken();
            AuthResponse response = _mapper.Map<AuthResponse>(account);
            if (response.RoleName == AppConstants.COMPANY)
                response.Id = account.Company.Id;
            else
                response.Id = account.Customer.Id;
            if (!await SaveRefreshToken(account, response.Id))
                throw new AuthException(AuthConstants.REFRESH_TOKEN_FAIL);
            
            
            response.Token = JwtUtils.GenerateToken(response);
            return response;
        }

        /// <summary>
        /// Change status account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        // public async Task<bool> ChangeStatus(AuthRequest request)
        // {
        //     try
        //     {
        //         AccountSpecification accountSpecification =
        //             new AccountSpecification("customer12", AppConstants.CUSTOMER);
        //         Account account = await _repository.Get(accountSpecification);
        //         await _repository.ChangeStatus(account, );
        //         return true;
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e.ToString());
        //         throw;
        //     }
        // }

        public Task<bool> Update(FormResetPass entity, int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            AuthResponse response = new AuthResponse();

            Account accountRequest = _mapper.Map<Account>(request);
            Account account = await GetAccountByUsername(request.Username) ?? throw new NotFoundException(AuthConstants.NOT_FOUND);
            account.RefreshToken = JwtUtils.GenerateRefreshToken();
            if (PassEncrypt.VerifyPassword(accountRequest.Password, account.Password))
            {
                if (true)
                {
                    response.Username = account.Username;
                    response.RoleName = account.Role.RoleName;
                    if (account.Company!= null)
                    {
                        response.Id = account.Company.Id;
                    }
                    else
                    {
                        response.Id = account.Customer.Id;
                        List<string> images = await _imageService.getImages(typeof(Customer).ToString(), account.Customer.Id);
                        if (images.Count > 0)
                        {
                            response.Avatar = images[0];
                        }
                    }

                    if (!await SaveRefreshToken(account, response.Id))
                        throw new ExceptionDetail(AuthConstants.ERROR);
                    response.Token = JwtUtils.GenerateToken(response);
                    response.RefreshToken = account.RefreshToken;
                    return response;
                }
            }

            throw new ExceptionDetail(AuthConstants.LOGIN_FAIL);
        }
        // public async Task<AccResponse> getAccByUsername(string Username, string roleName)
        // {
        //     AccountSpecification accountSpecification = new AccountSpecification(Username, roleName);
        //     Account account = await _repository.Get(accountSpecification);
        //
        //     AccResponse response = _mapper.Map<AccResponse>(account);
        //
        //     if (roleName == "COMPANY")
        //         response.Id = account.Company.Id;
        //     else 
        //         response.Id = account.Customer.Id;
        //     return response;
        // }

        public async Task<Account> GetAccountByUsername(string username, string roleName, bool checkStatus = true)
        {
            AccountSpecification accountSpecification = new AccountSpecification(username:username, roleName:roleName, checkStatus:checkStatus);
            Account account = await _repository.Get(accountSpecification, checkStatus: false);
            return account;
        }
        
        public async Task<Account> GetAccountByUsername(string username, bool checkStatus = true)
        {
            AccountSpecification specification = new AccountSpecification(username:username);
            Account account = await _repository.Get(specification, checkStatus: false);
            return account;
        }

        public async Task<Account> GetAccountByMail(string mail, bool checkStatus = true)
        {
            AccountSpecification specification = new AccountSpecification(mail:mail, checkStatus: checkStatus );
            Account account = await _repository.Get(specification, checkStatus: checkStatus);
            return account;
        }

        public async Task<AuthResponse> GoogleLogin(string mail)
        {
            AuthResponse response = new AuthResponse();
            Account account = await GetAccountByMail(mail:mail) ?? throw new NotFoundException(AuthConstants.NOT_FOUND);
            account.RefreshToken = JwtUtils.GenerateRefreshToken();
            if (true)
            {
                response.Username = account.Username;
                response.RoleName = account.Role.RoleName;
                if (account.Company!= null)
                {
                    response.Id = account.Company.Id;
                }
                else
                {
                    response.Id = account.Customer.Id;
                }

                if (!await SaveRefreshToken(account, response.Id))
                    throw new ExceptionDetail(AuthConstants.ERROR);
                response.Token = JwtUtils.GenerateToken(response);
                response.RefreshToken = account.RefreshToken;
                return response;
            }
        }

        #endregion -- Public Method --

        #region -- Private Method --

        private async Task<bool> SaveRefreshToken(Account account, int userId)
        {
            await _repository.Update(account, userId: userId);
            return true;
        }
        #endregion -- Private Method --
    }
}
