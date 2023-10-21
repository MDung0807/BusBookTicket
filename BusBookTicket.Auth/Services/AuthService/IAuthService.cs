using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;

namespace BusBookTicket.Auth.Services.AuthService
{
    public interface IAuthService : IService<AuthRequest, AuthRequest, int, AuthResponse>
    {
        Task<AuthResponse> login(AuthRequest request);
        Task<AccResponse> getAccByUsername(string username, string roleName);
        Task<Account> getAccountByUsername(string username, string rolleName);
    }
}
