using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class PricesConfigs: BaseEntityConfigs, IEntityTypeConfiguration<Prices>
{
    public void Configure(EntityTypeBuilder<Prices> builder)
    {
        builder.HasOne(x => x.Routes)
            .WithMany(x => x.Prices)
            .HasForeignKey("RouteId");

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Prices)
            .HasForeignKey("CompanyId");
        
        builder.HasIndex("RouteId", "CompanyId").IsUnique();

    }
}