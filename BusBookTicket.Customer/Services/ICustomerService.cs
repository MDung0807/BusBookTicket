using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Paging;
using OtpRequest = BusBookTicket.Application.OTP.Models.OtpRequest;

namespace BusBookTicket.CustomerManage.Services
{
    public interface ICustomerService : IService<FormRegister,FormUpdate, int, ProfileResponse, CustomerPaging, CustomerPagingResult>
    {
        Task<CustomerPagingResult> GetAllCustomer(CustomerPaging paging);
        Task<bool> AuthOtp(OtpRequest request);
    }
}
