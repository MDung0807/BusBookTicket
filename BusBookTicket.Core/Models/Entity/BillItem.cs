namespace BusBookTicket.Core.Models.Entity
{
    public class BillItem : BaseEntity
    {
        #region -- Properties --
        #endregion -- Properties --

        #region -- Relationship --
        public Bill? Bill { get; set; }
        public TicketItem TicketItem { get; set; }
        #endregion -- Relationship --


    }
}
