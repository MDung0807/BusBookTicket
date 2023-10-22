using BusBookTicket.Core.Common;


namespace BusBookTicket.Auth.Exceptions
{
    public class AuthException : ExceptionDetail
    {
        public AuthException(string message) : base(message) { }
    }
}
