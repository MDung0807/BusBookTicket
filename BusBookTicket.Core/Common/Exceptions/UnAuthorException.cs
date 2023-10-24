using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Common.Exceptions
{
    public class UnAuthorException : ExceptionDetail
    {
        public UnAuthorException(string message): base(message) { }
    }
}
