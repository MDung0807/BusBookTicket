using BusBookTicket.Auth.Security;
using BusBookTicket.Common.Common;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilitis;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public Response<string> register([FromBody] FormRegister register)
        {
            bool status = _customerService.create(register);
            string mess;
            if (status)
            {
                mess = CusConstants.REGISTER_SUCCESS;
            }
            else
                mess = CusConstants.REGISTER_FAIL;
            return new Response<string>(status, mess);
        }

        [HttpGet("profile")]
        [Authorize(Roles = "CUSTOMER")]
        public Response<string> getProfile()
        {
            var id = JwtUtils.GetUserID(HttpContext);
            bool status = false;
            string mess;
            if (status)
            {
                mess = CusConstants.REGISTER_SUCCESS;
            }
            else
                mess = CusConstants.REGISTER_FAIL;
            return new Response<string>(status, mess);
        }
    }
}
