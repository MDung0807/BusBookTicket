using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Exceptions
{
    public class AuthException : Exception
    {
        public string message { get; set; }
        public AuthException() { }

        public AuthException(string message)
        {
            this.message = message;
        }
    }
}
