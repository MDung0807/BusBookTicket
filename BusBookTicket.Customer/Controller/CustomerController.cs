using BusBookTicket.Common.Common;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilitis;
using Microsoft.AspNetCore.Mvc;


namespace BusBookTicket.CustomerManage.Controller
{
    [ApiController]
    [Route("")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController (ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public Response<string> register([FromBody] FormRegister register)
        {
            bool status = _customerService.create(register);
            string mess;
            if (status)
            {
                mess = CusContrain.REGISTER_SUCCESS;
            }
            else
                mess = CusContrain.REGISTER_FAIL;
            return new Response<string>(status, mess);
        }
    }
}
