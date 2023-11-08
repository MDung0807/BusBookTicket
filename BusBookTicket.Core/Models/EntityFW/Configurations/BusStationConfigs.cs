using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusStationConfigs: BaseEntityConfigs, IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {

            #region -- properties --
            builder.Property(x => x.Name)
                .HasMaxLength(50);
            builder.Property(x => x.Address)
                .HasMaxLength(50);
            builder.Property(x => x.Description);
            #endregion -- properties --

            #region -- relationship ---

            builder.HasOne(x => x.Ward)
                .WithMany(x => x.BusStations)
                .HasForeignKey("wardId");

            #endregion -- relationship ---
        }
    }
}
