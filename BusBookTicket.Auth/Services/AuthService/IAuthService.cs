using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Auth.Services.AuthService
{
    public interface IAuthService : IService<AuthRequest, FormResetPass, int, AuthResponse>
    {
        Task<AuthResponse> login(AuthRequest request);
        Task<AccResponse> getAccByUsername(string username, string roleName);
        Task<Account> getAccountByUsername(string username, string rolleName);
        Task<bool> resetPass(FormResetPass request);
    }
}
