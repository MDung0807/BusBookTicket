using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class TicketItemConfigs : BaseEntityConfigs, IEntityTypeConfiguration<TicketItem>
    {
        public void Configure(EntityTypeBuilder<TicketItem> builder)
        {

            #region -- Properties --
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.Ticket)
                .WithMany(x => x.TicketItems)
                .HasForeignKey("TicketID")
                .IsRequired();

            #endregion -- Relationship --
        }
    }
}
