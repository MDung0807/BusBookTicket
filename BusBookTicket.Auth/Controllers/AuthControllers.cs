using BusBookTicket.Application.SMS;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Auth.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController  : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #region -- Controller --

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var validator = new AuthRequestValidator();
            SMSService smsService = new SMSService();
            smsService.SendSms("+84376177512", "Hello mày");
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            request.RoleName = AppConstants.CUSTOMER;
            AuthResponse response = await _authService.Login(request);
            // await _authService.ChangeStatus(request);
            //var attemt = HttpContext.Session.GetInt32("FailLogin") ?? 0;
            
            return Ok(new Response<AuthResponse>(false, response));
        }
        
        [HttpPost("companies/login")]
        public async Task<IActionResult> CompanyLogin([FromBody] AuthRequest request)
        {
            var validator = new AuthRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            request.RoleName = AppConstants.COMPANY;
            AuthResponse response = await _authService.Login(request);
            return Ok(new Response<AuthResponse>(false, response));
        }
        
        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] AuthRequest request)
        {
            var validator = new AuthRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            request.RoleName = AppConstants.ADMIN;
            AuthResponse response = await _authService.Login(request);
            return Ok(new Response<AuthResponse>(false, response));
        }
        
        // [HttpPost("confirmAccount")]
        // public async Task<IActionResult> confirmAccount( [FromBody] )
        // {
        //     bool response = await _authService.resetPass(requets);
        //     return Ok(new Response<string>(false, "response"));
        // }
        // #endregion -- Controller --

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] FormResetPass request)
        {
            var validator = new FormResetPassValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            
            bool response = await _authService.ResetPass(request, -1);
            return Ok(new Response<string>(false, "response"));
        }
        
        [HttpPost("refreshToken")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {                   
            AuthResponse response = new AuthResponse();
            response = await _authService.RefreshToken(request: request);
            return Ok(new Response<AuthResponse>(false, response));
        } 
        #endregion -- Controller --
    }
}
