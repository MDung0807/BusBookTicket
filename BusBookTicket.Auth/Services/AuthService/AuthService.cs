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
        private Account _account;
        #endregion -- Properties --

        #region -- Public Method --
        public AuthService(IAuthRepository authRepository, IMapper mapper, IRoleService roleService)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public bool create(AuthRequest request)
        {
            _account = _mapper.Map<Account>(request);
            Role role = _roleService.getRole(request.roleName);
            _account.role = role;
            _account.status = 1;

            _authRepository.create(_account);
            return true;
        }

        public bool update(AuthRequest entity, int id)
        {
            throw new NotImplementedException();
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AuthResponse> getAll()
        {
            throw new NotImplementedException();
        }

        public AuthResponse getByID(int id)
        {
            throw new NotImplementedException();
        }

        public AuthResponse login(AuthRequest request)
        {
            AuthResponse response = new AuthResponse() ;

            _account = _mapper.Map<Account>(request);
            if (_authRepository.login(_account))
            {
                _account = _authRepository.getAccByUsername(request.username, request.roleName);
                
                if (_account.role.roleName == request.roleName)
                {
                    response.username = _account.username;
                    response.roleName = _account.role.roleName;
                    if (request.roleName == "COMPANY")
                    {
                        response.userID = _account.company.companyID;
                    }
                    else
                    {
                        response.userID = _account.customer.customerID;
                    }
                    response.token = JwtUtils.GernerateToken(response);
                    return response;
                }
            }

            throw new AuthException(AuthConstants.LOGIN_FAIL);
        }
        public AccResponse getAccByUsername(string username, string roleName)
        {
            Account account = _authRepository.getAccByUsername(username, roleName);
            AccResponse response = new AccResponse() ;
            response.username = account.username;
            response.roleName = account.role.roleName ;

            if (roleName == "COMPANY")
                response.userID = account.company.companyID;
            else 
                response.userID = account.customer.customerID;
            return response;
        }

        public Account getAccountByUsername(string username, string roleName)
        {
            return _authRepository.getAccByUsername(username, roleName);
        }
        #endregion -- Public Method --

        #region -- Private Method --
        #endregion -- Private Method --
    }
}
