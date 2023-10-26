using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class TicketItemConfigs : IEntityTypeConfiguration<TicketItem>
    {
        public void Configure(EntityTypeBuilder<TicketItem> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.ticketItemID);

            builder.Property(x => x.ticketItemID)
                .ValueGeneratedOnAdd();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.ticket)
                .WithMany(x => x.TicketItems)
                .HasForeignKey("ticketID")
                .IsRequired();

            #endregion -- Relationship --
        }
    }
}
