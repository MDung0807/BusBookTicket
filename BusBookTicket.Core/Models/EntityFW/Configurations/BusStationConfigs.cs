using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusStationConfigs: IEntityTypeConfiguration<BusStation>
    {
        public void Configure(EntityTypeBuilder<BusStation> builder)
        {

            #region -- properties --
            builder.HasKey(x => x.busStationID);

            builder.Property(x => x.busStationID)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.name)
                .HasMaxLength(50);
            builder.Property(x => x.address)
                .HasMaxLength(50);
            builder.Property(x => x.description);
            #endregion -- properties --

            #region -- relationship ---
            
            #endregion -- relationship ---
        }
    }
}
