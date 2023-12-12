namespace BusBookTicket.Core.Models.Entity
{
    public class Review : BaseEntity
    {
        #region -- Properties --
        public string? Reviews { get; set; }
        public string? Image { get; set; }
        public int Rate { get; set; }
        #endregion -- Properties --

        #region -- Relationship --
        public Customer? Customer { get; set; }
        public Bus Bus { get; set; }
        #endregion -- Relationship --

    }
}
