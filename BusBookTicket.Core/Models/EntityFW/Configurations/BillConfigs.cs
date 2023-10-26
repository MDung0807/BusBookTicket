using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BillConfigs : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.billID);

            builder.Property(x => x.billID)
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
