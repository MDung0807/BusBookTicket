using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    public class ExceptionDetail : Exception
    {
        public string message { get; set; }
        public ExceptionDetail() { }

        public ExceptionDetail(string message)
        {
            this.message = message;
        }

        public ExceptionDetail (string message, Exception detail) :base (message, detail) { }
    }
}
