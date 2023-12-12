using BusBookTicket.Core.Common.Exceptions;


namespace BusBookTicket.Auth.Exceptions
{
    public class AuthException : ExceptionDetail
    {
        public AuthException(string message) : base(message) { }
    }
}
