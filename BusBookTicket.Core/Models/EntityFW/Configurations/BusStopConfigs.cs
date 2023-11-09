using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusStopConfigs : BaseEntityConfigs, IEntityTypeConfiguration<BusStop>
    {
        public void Configure(EntityTypeBuilder<BusStop> builder)
        {

            #region -- Properties --

            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.BusStation)
                .WithMany(x => x.BusStops)
                .HasForeignKey("BusStationID")
                .IsRequired();
            builder.HasOne(x => x.Bus)
                .WithMany(x => x.BusStops)
                .HasForeignKey("BusID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
