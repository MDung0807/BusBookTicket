using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class TicketConfigs : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.ticketID);

            builder.Property(x => x.ticketID)
                .ValueGeneratedOnAdd();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.busStationStart)
                .WithMany(x => x.ticketStarts)
                .HasForeignKey("busStationStartID")
                .IsRequired();
            builder.HasOne(x => x.busStationEnd)
                .WithMany(x => x.ticketends)
                .HasForeignKey("busStationEndID")
                .IsRequired();

            builder.HasOne(x => x.customer)
               .WithMany(x => x.tickets)
               .HasForeignKey("customerID")
               .IsRequired();

            builder.HasOne(x => x.discount)
               .WithMany(x => x.tickets)
               .HasForeignKey("discountID")
               .IsRequired() ;
            #endregion -- Relationship --
        }
    }
}
