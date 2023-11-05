using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Models.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }

        #region -- Relationship --
        public HashSet<Account> Accounts { get; set; }
        #endregion -- Relationship --
    }
}
