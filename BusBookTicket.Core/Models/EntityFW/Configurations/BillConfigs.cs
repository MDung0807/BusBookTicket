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
                .HasForeignKey("BusStationStartID")
                .IsRequired();
            builder.HasOne(x => x.BusStationEnd)
                .WithMany(x => x.TicketEnds)
                .HasForeignKey("BusStationEndID")
                .IsRequired();

            builder.HasOne(x => x.Customer)
               .WithMany(x => x.Tickets)
               .HasForeignKey("CustomerID")
               .IsRequired();

            builder.HasOne(x => x.Discount)
               .WithMany(x => x.Tickets)
               .HasForeignKey("DiscountID")
               .IsRequired(false) ;
            #endregion -- Relationship --
        }
    }
}
