using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Role
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
        public string description { get; set; }
        public int status { get; set; }

        #region -- Relationship --
        public HashSet<Account> accounts { get; set; }
        #endregion -- Relationship --
    }
}
