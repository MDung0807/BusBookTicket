using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Repositories;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services
{
    public class AuthService : IAuthService
    {
        #region -- Properties --
        private IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private Account _account;
        #endregion -- Properties --

        #region -- Public Method --
        public AuthService(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public bool create(Account account)
        {
            _account = _mapper.Map<Account>(account);
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
                _account =_authRepository.getAccByUsername(request.username);
                string token = JwtUtils.GernerateToken(_account.username, _account.role.roleName);
                response = new AuthResponse(_account.accountID, _account.username, token);
            }
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
