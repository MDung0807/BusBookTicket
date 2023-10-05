using BusBookTicket.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Exceptions
{
    public class AuthException : ExceptionDetail
    {
        public AuthException(string message) : base(message) { }
    }
}
