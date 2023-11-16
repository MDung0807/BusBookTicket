using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Auth.Services.AuthService
{
    public interface IAuthService : IService<AuthRequest, FormResetPass, int, AuthResponse>
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<Account> GetAccountByUsername(string username, string rolleName);
        Task<bool> ResetPass(FormResetPass request);
        // Task<bool> ChangeStatus(AuthRequest request);
    }
}
