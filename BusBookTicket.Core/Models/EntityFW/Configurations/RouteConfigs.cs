using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class RouteConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Routes>
{
    public void Configure(EntityTypeBuilder<Routes> builder)
    {
        #region --Relationship --


        builder.HasOne(x => x.BusStationStart)
            .WithMany(x => x.StationStart)
            .HasForeignKey("StationStartId")
            .IsRequired();

        builder.HasOne(x => x.BusStationEnd)
            .WithMany(x => x.StationEnd)
            .HasForeignKey("StationEndId")
            .IsRequired();
        #endregion --Relationship --
    }
}