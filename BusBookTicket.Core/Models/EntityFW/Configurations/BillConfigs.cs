using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BillConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {

            #region -- Properties --
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.BusStationStart)
                .WithMany(x => x.TicketStarts)
                .HasForeignKey("busStationStartID")
                .IsRequired();
            builder.HasOne(x => x.BusStationEnd)
                .WithMany(x => x.TicketEnds)
                .HasForeignKey("busStationEndID")
                .IsRequired();

            builder.HasOne(x => x.Customer)
               .WithMany(x => x.Tickets)
               .HasForeignKey("customerID")
               .IsRequired();

            builder.HasOne(x => x.Discount)
               .WithMany(x => x.Tickets)
               .HasForeignKey("discountID")
               .IsRequired() ;
            #endregion -- Relationship --
        }
    }
}
