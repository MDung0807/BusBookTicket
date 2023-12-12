using BusBookTicket.Core.Common.Exceptions;

namespace BusBookTicket.CustomerManage.Exceptions
{
    public class CustomerException : ExceptionDetail
    {
        public CustomerException(string message) : base(message)
        {
        }
    }
}
