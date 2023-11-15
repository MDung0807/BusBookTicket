
namespace BusBookTicket.Core.Common
{
    public class ExceptionDetail : Exception
    {
        public string message { get; set; }

        public ExceptionDetail(string message)
        {
            this.message = message;
        }
        public ExceptionDetail(){}

        public ExceptionDetail (string message, Exception detail) :base (message, detail) { }
    }
}
