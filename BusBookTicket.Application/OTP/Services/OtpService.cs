using AutoMapper;
using BusBookTicket.Application.OTP.Models;
using BusBookTicket.Application.OTP.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Application.OTP.Services;

public class OtpService : IOtpService
{
    private readonly IGenericRepository<OtpCode> _repository;
    private readonly IMapper _mapper;

    public OtpService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _repository = unitOfWork.GenericRepository<OtpCode>();
    }
    public async Task<OtpResponse> CreateOtp(OtpRequest request, int userId)
    {
        OtpSpecification specification = new OtpSpecification(userId:userId, request.Email);
        OtpCode otp = await _repository.Get(specification);
        if (otp == null)
            otp = new OtpCode();
        otp.Email = request.Email;
        otp.UserId = userId;
        string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };  
  
        string sRandomOtp = GenerateRandomOtp(8, saAllowedCharacters);
        otp.Code = sRandomOtp;
        otp.Status = (int)EnumsApp.Active;

        otp = await _repository.CreateOrUpdate(otp, userId);
        return _mapper.Map<OtpCode, OtpResponse>(otp);
    }

    public async Task<bool> AuthenticationOtp(OtpRequest request, int userId)
    {
        DateTime dateTimeNow = DateTime.Now;
        int expired = 10; // expired 10 minute
        OtpSpecification specification = new OtpSpecification(userId, request.Email, dateTime:dateTimeNow, minute:expired, false);
        OtpCode otp = await _repository.Get(specification);
        if (otp != null && otp.Code == request.Code)
            return true;
        return false;
    }
    
    private string GenerateRandomOtp(int iOTPLength,string[] saAllowedCharacters )  
    {  
  
        string sOtp = String.Empty;  
  
        string sTempChars = String.Empty;  
  
        Random rand = new Random();  
  
        for (int i = 0; i < iOTPLength; i++)  
  
        {  
  
            int p = rand.Next(0, saAllowedCharacters.Length);  
  
            sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];  
  
            sOtp += sTempChars;  
  
        }  
  
        return sOtp;  
  
    }  

}