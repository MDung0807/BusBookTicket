using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.OTP.Specification;

public class OtpSpecification : BaseSpecification<OtpCode>
{
    public OtpSpecification( int userId, string email, DateTime dateTime, int minute, bool checkStatus = true) 
        : base(x => x.UserId == userId && x.Email == email && x.DateUpdate.AddMinutes(minute) >  dateTime,
            checkStatus:checkStatus)
    {
        
    }
    
    public OtpSpecification( int userId, string email, bool checkStatus = true) 
        : base(x => x.Email == email,
            checkStatus:checkStatus)
    {
        
    }
}