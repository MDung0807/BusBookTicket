using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    
    public class Response<T>
    {
        public bool isError { get; set; }
        public T data { get; set; }

        public Response(bool isError, T data)
        {
            this.isError = isError;
            this.data = data;
        }
    }
}
