using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Models.EntityFW.Configurations
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
                .WithMany(x => x.ticketItems)
                .HasForeignKey("ticketID");

            builder.HasOne(x => x.seat)
                .WithOne(x => x.ticketItem)
                .HasForeignKey<TicketItem>("seatID");
            #endregion -- Relationship --
        }
    }
}
