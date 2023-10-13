using BusBookTicket.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.Exceptions
{
    public class CustomerException : ExceptionDetail
    {
        public CustomerException(string message) : base(message)
        {
        }
    }
}
