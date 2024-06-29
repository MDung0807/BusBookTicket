using BusBookTicket.Application.OTP.Models;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Security;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Paging;
using BusBookTicket.CustomerManage.Services;
using BusBookTicket.CustomerManage.Utilities;
using BusBookTicket.CustomerManage.Validator;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.CustomerManage.Controller
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthService _authService;
        public CustomerController(ICustomerService customerService, IAuthService authService)
        {
            _customerService = customerService;
            _authService = authService;
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

        [HttpGet("find")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Find([FromQuery] string param,[FromQuery] CustomerPaging paging)
        {
            CustomerPagingResult responses =  await _customerService.FindByParam(param, paging);
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

        [HttpPut("changeIsActive")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeIsActive([FromQuery] int customerId)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status =  await _customerService.ChangeIsActive(customerId, userId);
            return Ok(new Response<string>(!status, "responses"));
        }
        
        [HttpPut("changeIsDelete")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangeIsDelete([FromQuery] int customerId)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status =  await _customerService.Delete(customerId, userId);
            return Ok(new Response<string>(!status, "responses"));
        }
        
        [HttpPut("ChangeIsLock")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangIsLock([FromQuery] int customerId)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status =  await _customerService.ChangeIsLock(customerId, userId);
            return Ok(new Response<string>(!status, "responses"));
        }
        
        [HttpPost("loginOnGoogle")]
        public async Task<IActionResult> GoogleLogin([FromBody] TokenGoogle token)
        {
            try
            {
                // Validate the token received from the frontend
                var payload = await GoogleJsonWebSignature.ValidateAsync(token.Token);

                var account = await _authService.GetAccountByMail(payload.Email);
                if (account == null)
                {
                    FormRegister register = new FormRegister
                    {
                        Email = payload.Email,
                        FullName = payload.Name,
                        RoleName = AppConstants.CUSTOMER,
                        Username = payload.Email
                    };
                    await _customerService.CreateByGoogle(register);
                    account= await _authService.GetAccountByMail(payload.Email, checkStatus:false);

                    await _customerService.ChangeIsActive(account.Customer.Id, account.Customer.Id);
                }
                var response = await _authService.GoogleLogin(mail: payload.Email);
                response.Avatar = payload.Picture;
                // Here you can create your own JWT token or handle user authentication
                // For demonstration, we'll just return the user info
                return Ok(new Response<AuthResponse>(false, response));
            }
            catch (InvalidJwtException)
            {
                return Unauthorized();
            }
        }
    }
}
