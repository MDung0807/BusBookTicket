using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.DTOs.Responses;
using BusBookTicket.Auth.Repositories;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services
{
    public interface IAuthService : IService<Account, AuthRequest, AuthResponse>
    {
        AuthResponse login (AuthRequest request);
        Account getAccByUsername (string username);
    }
}
