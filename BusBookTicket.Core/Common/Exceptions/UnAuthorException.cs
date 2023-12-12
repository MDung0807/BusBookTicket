namespace BusBookTicket.Core.Common.Exceptions
{
    public class UnAuthorException : ExceptionDetail
    {
        public UnAuthorException(string message): base(message) { }
    }
}
