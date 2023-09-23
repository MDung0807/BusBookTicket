using BusBookTicket.Common.Common;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;

namespace BusBookTicket.CustomerManage.Services
{
    public interface ICustomerService : IService<FormRegister,FormUpdate, CustomerResponse>
    {

    }
}
