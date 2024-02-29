
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Auth.Services.AuthService
{
    public interface IAuthService : IService<AuthRequest, FormResetPass, int, AuthResponse, object, object>
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<Account> GetAccountByUsername(string username, string roleName, bool checkStatus = true);
        Task<bool> ResetPass(FormResetPass request, int userId);
        // Task<bool> ChangeStatus(AuthRequest request);

        Task<AuthResponse> RefreshToken(RefreshTokenRequest request);
        Task<Account> GetAccountByUsername(string username, bool checkStatus = true);
    }
}
