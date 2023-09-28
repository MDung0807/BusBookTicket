using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Repositories.AuthRepository;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Services.RoleService;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services.AuthService
{
    public class AuthService : IAuthService
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
            return _authRepository.create(_account);
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AuthResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public AuthResponse getByID(int id)
        {
            throw new NotImplementedException();
        }

        public AuthResponse login(AuthRequest request)
        {
            AuthResponse response;
            _account = _mapper.Map<Account>(request);
            if (_authRepository.login(_account))
            {
                _account = _authRepository.getAccByUsername(request.username);
                if (_account.role.roleName == request.roleName)
                {
                    string token = JwtUtils.GernerateToken(_account.username, _account.role.roleName);
                    response = new AuthResponse(_account.accountID, _account.username, token, _account.role.roleName);
                    return response;
                }
            }

            throw new AuthException(AuthContrains.LOGIN_FAIL);
        }

        public AuthResponse update(AuthRequest entity)
        {
            throw new NotImplementedException();
        }

        public Account getAccByUsername(string username)
        {
            return _authRepository.getAccByUsername(username);
        }
        #endregion -- Public Method --
    }
}
