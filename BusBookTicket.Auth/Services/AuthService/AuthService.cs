using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Repositories.AuthRepository;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Models.Entity;

namespace BusBookTicket.Auth.Services.AuthService
{
    public sealed class AuthService : IAuthService
    {
        #region -- Properties --
        private IAuthRepository _authRepository;
        private IRoleService _roleService;
        private readonly IMapper _mapper;
        #endregion -- Properties --

        #region -- Public Method --
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="roleService"></param>
        public AuthService(IAuthRepository authRepository, IMapper mapper, IRoleService roleService)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<bool> create(AuthRequest request)
        {
            Account account = _mapper.Map<Account>(request);
            Role role = await _roleService.getRole(request.roleName);
            account.role = role;
            account.status = 1;

            _authRepository.create(account);
            return true;
        }

        public Task<bool> update(AuthRequest entity, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthResponse>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> getByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResponse> login(AuthRequest request)
        {
            AuthResponse response = new AuthResponse() ;

            Account account = _mapper.Map<Account>(request);
            if (await _authRepository.login(account))
            {
                account = await _authRepository.getAccByUsername(request.username, request.roleName);
                
                if (account.role.roleName == request.roleName)
                {
                    response.username = account.username;
                    response.roleName = account.role.roleName;
                    if (request.roleName == "COMPANY")
                    {
                        response.userID = account.company.companyID;
                    }
                    else
                    {
                        response.userID = account.customer.customerID;
                    }
                    response.token = JwtUtils.GernerateToken(response);
                    return response;
                }
            }

            throw new AuthException(AuthConstants.LOGIN_FAIL);
        }
        public async Task<AccResponse> getAccByUsername(string username, string roleName)
        {
            Account account = await _authRepository.getAccByUsername(username, roleName);

            AccResponse response = _mapper.Map<AccResponse>(account);

            if (roleName == "COMPANY")
                response.userID = account.company.companyID;
            else 
                response.userID = account.customer.customerID;
            return response;
        }

        public async Task<Account> getAccountByUsername(string username, string roleName)
        {
            return await _authRepository.getAccByUsername(username, roleName);
        }
        #endregion -- Public Method --

        #region -- Private Method --
        #endregion -- Private Method --
    }
}
