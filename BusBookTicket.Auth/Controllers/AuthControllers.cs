using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Security;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Auth.Controllers
{
    [Route("auth")]
    public class AuthController  : ControllerBase
    {
       
        [HttpGet("getAccount")]
        public Response<Account> get()
        {
            Account account = new Account();
            account.username = "ffff";
            account.password = "password";
            account.accountID = 3;
            return new Response<Account>(false, account);
        }

        [HttpPost("login")]
        public Response<AuthResponse> login([FromBody] AuthRequest requets)
        {
            string token = JwtUtils.GernerateToken(requets.username);
            AuthResponse response = new AuthResponse();
            response.token = token;
            response.role = "User";
            response.userName = requets.username;
            response.userID = 1;

            return new Response<AuthResponse>(false, response);
        }

        [HttpPost("testToken")]
        public Response<string> test (string token)
        {
             var status = JwtUtils.GetPrincipal(token);
            return new Response<string>(false, status.ToString());
        }
        
    }
}
