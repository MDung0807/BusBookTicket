
namespace BusBookTicket.Core.Models.Entity
{
    public class Account
    {
        #region -- Properties --
        public int accountID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        #endregion -- Properties --

        #region -- Relationship ---
        public Customer customer { get; set; }
        public Company company { get; set; }
        public Role role { get; set; }
        #endregion -- Relationship ---

    }
}
