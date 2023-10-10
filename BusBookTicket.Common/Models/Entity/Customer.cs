using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Models.Entity
{
    public class Customer
    {
        #region -- configs property --

        public int customerID { get; set; }
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public DateTime dateCreate { get; set; }
        public DateTime dateUpdate { get; set; }
        public int status { get; set; }
        #endregion -- configs property --


        #region -- RelationShip--

        public Account? account { get; set; }
        public HashSet<Review>? reviews { get; set; }
        public HashSet<Ticket>?tickets { get; set; }
        public Rank rank { get; set; }
        #endregion -- RelationShip --
    }
}
