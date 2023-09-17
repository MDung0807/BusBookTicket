﻿using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Auth.Controllers
{
    [Route("account")]
    public class AccountController  : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "áaa";
        }

        [HttpGet("getAccount")]
        public Response<Account> get()
        {
            Account account = new Account();
            account.username = "ffff";
            account.password = "password";
            account.accountID = 3;
            return new Response<Account>(false, account);
        }

        
    }
}