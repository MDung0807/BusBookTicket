using BusBookTicket.Application.OTP.Models;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.OTP.Services;

public class OtpService : IOtpService
{
    private readonly IGenericRepository<OtpCode> _repository;

    public OtpService(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.GenericRepository<OtpCode>();
    }
    public Task<OtpResponse> CreateOtp(OtpRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AuthenticationOtp(OtpRequest request)
    {
        throw new NotImplementedException();
    }
}