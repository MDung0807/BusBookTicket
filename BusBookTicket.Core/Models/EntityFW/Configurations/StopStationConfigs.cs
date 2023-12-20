using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class StopStationConfigs : BaseEntityConfigs, IEntityTypeConfiguration<StopStation>
{
    public void Configure(EntityTypeBuilder<StopStation> builder)
    {
        builder.HasOne(x => x.Bus)
            .WithMany(x => x.StopStations)
            .HasForeignKey("BusId");
        

        builder.HasOne(x => x.RouteDetail)
            .WithOne(x => x.StopStation)
            .HasForeignKey("RouteDetail");

    }
}