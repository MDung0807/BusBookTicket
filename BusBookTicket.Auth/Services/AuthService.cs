using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Repositories;
using BusBookTicket.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services
{
    public class AuthService : IAuthService
    {
       public bool create (AccountRequest request)
        {
            return false;
        }
    }
}
