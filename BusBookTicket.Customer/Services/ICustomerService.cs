using BusBookTicket.Core.Common;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;

namespace BusBookTicket.CustomerManage.Services
{
    public interface ICustomerService : IService<FormRegister,FormUpdate, int, ProfileResponse>
    {
        Task<List<CustomerResponse>> getAllCustomer();
    }
}
