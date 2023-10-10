using BusBookTicket.Common.Common;


namespace BusBookTicket.Auth.Exceptions
{
    public class AuthException : ExceptionDetail
    {
        public AuthException(string message) : base(message) { }
    }
}
