using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Repositories;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services.AuthService
{
    public interface IAuthService : IService<AuthRequest, AuthRequest, AuthResponse>
    {
        AuthResponse login(AuthRequest request);
        AccResponse getAccByUsername(string username, string roleName);
        Account getAccountByUsername(string username, string rolleName);
    }
}
