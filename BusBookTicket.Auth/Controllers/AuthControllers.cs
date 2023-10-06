using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Common.Common;
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
        public IActionResult login([FromBody] AuthRequest requets)
        {
            AuthResponse response = _authService.login(requets);
            return Ok(new Response<AuthResponse>(false, response));
        }
        #endregion -- Controller --
    }
}
