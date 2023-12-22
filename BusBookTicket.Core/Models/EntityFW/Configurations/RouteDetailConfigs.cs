using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class RouteDetailConfigs:  BaseEntityConfigs, IEntityTypeConfiguration<RouteDetail>
{
    public void Configure(EntityTypeBuilder<RouteDetail> builder)
    {
        builder.HasOne(x => x.Routes)
            .WithMany(x => x.RouteDetails)
            .HasForeignKey("RouteId");
        
        builder.HasOne(x => x.Station)
            .WithMany(x => x.RouteDetails)
            .HasForeignKey("StationId");
        
        builder.HasOne(x => x.Company)
            .WithMany(x => x.RouteDetails)
            .HasForeignKey("CompanyId");
    }
}