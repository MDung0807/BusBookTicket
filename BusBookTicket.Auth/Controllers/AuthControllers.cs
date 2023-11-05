using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Common;
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
        public async Task<IActionResult> login([FromBody] AuthRequest requets)
        {
            requets.roleName = AppConstants.CUSTOMER;
            AuthResponse response = await _authService.Login(requets);
            return Ok(new Response<AuthResponse>(false, response));
        }
        
        [HttpPost("company/login")]
        public async Task<IActionResult> companyLogin([FromBody] AuthRequest requets)
        {
            requets.roleName = AppConstants.COMPANY;
            AuthResponse response = await _authService.Login(requets);
            return Ok(new Response<AuthResponse>(false, response));
        }
        
        [HttpPost("admin/login")]
        public async Task<IActionResult> adminLogin([FromBody] AuthRequest requets)
        {
            requets.roleName = AppConstants.ADMIN;
            AuthResponse response = await _authService.Login(requets);
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
        public async Task<IActionResult> resetPassword([FromBody] FormResetPass requets)
        {
            bool response = await _authService.ResetPass(requets);
            return Ok(new Response<string>(false, "response"));
        }
        #endregion -- Controller --
    }
}
