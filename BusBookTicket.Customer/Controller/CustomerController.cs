using AutoMapper;
using BusBookTicket.Auth.Security;
using BusBookTicket.Common.Common;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilitis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.CustomerManage.Controller
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
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
            return new Response<string>(!status, mess);
        }

        [HttpGet("profile")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult getProfile()
        {
            int id = JwtUtils.GetUserID(HttpContext);
            ProfileResponse response = _customerService.getByID(id);
            return Ok(new Response<ProfileResponse>(false, response));
        }
        
        [HttpGet("getAll")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult getAllCustomer()
        {
            List<CustomerResponse> responses = new List<CustomerResponse>();
            responses = _customerService.getAllCustomer();
            return Ok(new Response<List<CustomerResponse>>(false, responses));
        }

        [HttpPost("updateProfile")]
        [Authorize(Roles = "CUSTOMER")]
        public IActionResult updateProfile([FromBody] FormUpdate request)
        {
            int id = JwtUtils.GetUserID(HttpContext);
            bool status = _customerService.update(request, id);
            return Ok(new Response<string>(false, "response"));
        }

    }
}
