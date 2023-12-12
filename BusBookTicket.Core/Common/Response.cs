namespace BusBookTicket.Core.Common
{
    
    public class Response<T>
    {
        public bool isError { get; set; }
        public T data { get; set; }
        public string dateTime { get; set; }

        public Response(bool isError, T data)
        {
            this.isError = isError;
            this.data = data;
            this.dateTime = (DateTime.Now).ToString("dd/MM/yyyy hh:mm tt");
        }
    }
}
