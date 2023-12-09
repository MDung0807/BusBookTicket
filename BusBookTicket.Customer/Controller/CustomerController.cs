using BusBookTicket.Application.OTP.Models;
using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Paging;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilities;
using BusBookTicket.CustomerManage.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.CustomerManage.Controller
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult>Register([FromForm] FormRegister register)
        {
            var validator = new FormRegisterValidator();
            var result = await validator.ValidateAsync(register);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            register.RoleName = AppConstants.CUSTOMER;
            bool status = await _customerService.Create(register, -1);
            var mess = status ? CusConstants.REGISTER_SUCCESS : CusConstants.REGISTER_FAIL;

            return Ok(new Response<string>(!status, mess));
        }

        [HttpGet("profile")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> GetProfile()
        {
            int id = JwtUtils.GetUserID(HttpContext);
            ProfileResponse response = await _customerService.GetById(id);
            return Ok(new Response<ProfileResponse>(false, response));
        }
        
        [HttpGet("getAll")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllCustomer([FromQuery] CustomerPaging paging)
        {
            CustomerPagingResult responses =  await _customerService.GetAllCustomer(paging:paging);
            return Ok(new Response<CustomerPagingResult>(false, responses));
        }

        [HttpPut("updateProfile")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<IActionResult> UpdateProfile([FromBody] FormUpdate request)
        {
            var validator = new FormUpdateValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            int id = JwtUtils.GetUserID(HttpContext);
            bool status = await _customerService.Update(request, id, id);
            return Ok(new Response<string>(false, "response"));
        }

        [HttpPost("authOtp")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthOtpCode([FromBody] OtpRequest request)
        {
            bool status = await _customerService.AuthOtp(request);
            return Ok(new Response<string>(!status, "response"));
        }

    }
}
