using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Common;
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
            AuthResponse response = await _authService.login(requets);
            return Ok(new Response<AuthResponse>(false, response));
        }

        [HttpPost("reset")]
        public async Task<IActionResult> resetPassword([FromBody] FormResetPass requets)
        {
            bool response = await _authService.update(requets, 0);
            return Ok(new Response<string>(false, "response"));
        }
        #endregion -- Controller --
    }
}
