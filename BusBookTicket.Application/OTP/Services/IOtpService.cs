using BusBookTicket.Application.OTP.Models;

namespace BusBookTicket.Application.OTP.Services;

public interface IOtpService
{
    Task<OtpResponse> CreateOtp(OtpRequest request, int userId);
    Task<bool> AuthenticationOtp(OtpRequest request, int userId);
}