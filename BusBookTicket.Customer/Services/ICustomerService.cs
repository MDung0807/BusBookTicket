using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Paging;

namespace BusBookTicket.CustomerManage.Services
{
    public interface ICustomerService : IService<FormRegister,FormUpdate, int, ProfileResponse, CustomerPaging, CustomerPagingResult>
    {
        Task<CustomerPagingResult> GetAllCustomer(CustomerPaging paging);
    }
}
