using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Common.Models.EntityFW.Configurations
{
    public class BusStopConfigs : IEntityTypeConfiguration<BusStop>
    {
        public void Configure(EntityTypeBuilder<BusStop> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.busStopID);

            builder.Property(x => x.busStopID)
                .ValueGeneratedOnAdd();

            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.BusStation)
                .WithMany(x => x.busStops)
                .HasForeignKey("busStationID")
                .IsRequired();
            builder.HasOne(x => x.bus)
                .WithMany(x => x.busStops)
                .HasForeignKey("busID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
