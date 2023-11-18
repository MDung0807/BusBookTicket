using AutoMapper;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Auth.Specification;
using BusBookTicket.Auth.Utils;
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
            account.Status = (int) EnumsApp.Active;
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

        public Task<bool> ChangeIsWaiting(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeIsDisable(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIsExistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckIsExistByParam(string param)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ResetPass(FormResetPass request)
        {
            
            // request.password = PassEncrypt.hashPassword(request.password);
            // await _authRepository.resetPass(request);
            // return true;
            throw new NotImplementedException();
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
            Account account = await GetAccountByUsername(request.Username, request.RoleName);
            
            if (PassEncrypt.VerifyPassword(accountRequest.Password, account.Password))
            {
                if (account.Role.RoleName == request.RoleName)
                {
                    response.Username = account.Username;
                    response.RoleName = account.Role.RoleName;
                    if (request.RoleName == AppConstants.COMPANY)
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
                    response.Token = JwtUtils.GenerateToken(response);
                    return response;
                }
            }

            throw new AuthException(AuthConstants.LOGIN_FAIL);
        }
        // public async Task<AccResponse> getAccByUsername(string username, string roleName)
        // {
        //     AccountSpecification accountSpecification = new AccountSpecification(username, roleName);
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

        public async Task<Account> GetAccountByUsername(string username, string roleName)
        {
            AccountSpecification accountSpecification = new AccountSpecification(username, roleName);
            Account account = await _repository.Get(accountSpecification);
            return account;
        }
        #endregion -- Public Method --

        #region -- Private Method --
        #endregion -- Private Method --
    }
}
