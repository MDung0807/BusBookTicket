using AutoMapper;
using BusBookTicket.Auth.Security;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilitis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.CustomerManage.Controller
{
    [ApiController]
    [Authorize(Roles ="CUSTOMER")]
    [Route("api")]
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
        public IActionResult getProfile()
        {
            int id = JwtUtils.GetUserID(HttpContext);
            ProfileResponse response = _customerService.getByID(id);
            return  Ok(new Response<ProfileResponse>(false, response));
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet("getAll")]
        public IActionResult getAllCustomer()
        {
            List<CustomerResponse> responses = new List<CustomerResponse>();
            responses = _customerService.getAll();
            return Ok(new Response<List<CustomerResponse>>(false, responses));
        }
    }
}
