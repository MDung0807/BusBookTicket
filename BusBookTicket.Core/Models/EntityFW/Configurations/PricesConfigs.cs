using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class PricesConfigs: BaseEntityConfigs, IEntityTypeConfiguration<Prices>
{
    public void Configure(EntityTypeBuilder<Prices> builder)
    {
        builder.HasOne(x => x.Routes)
            .WithOne(x => x.Prices)
            .HasForeignKey<Prices>("RouteId");

        builder.HasOne(x => x.Company)
            .WithMany(x => x.Prices)
            .HasForeignKey("CompanyId");
    }
}