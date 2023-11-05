
namespace BusBookTicket.Core.Models.Entity
{
    public class Account: BaseEntity
    {
        #region -- Properties --
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion -- Properties --

        #region -- Relationship ---
        public Customer Customer { get; set; }
        public Company Company { get; set; }
        public Role Role { get; set; }
        #endregion -- Relationship ---

    }
}
