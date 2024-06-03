namespace BusBookTicket.Core.Models.Entity
{
    public class Customer : BaseEntity
    {
        #region -- configs property --

        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        #endregion -- configs property --


        #region -- RelationShip--

        public Account Account { get; set; }
        public HashSet<Review> Reviews { get; set; }
        public HashSet<Bill>Tickets { get; set; }
        public Rank Rank { get; set; }
        public Ward Ward { get; set; }
        public HashSet<Notification> Notifications { get; set; }
        public HashSet<NotificationChange> NotificationChanges { get; set; }
        #endregion -- RelationShip --
    }
}
