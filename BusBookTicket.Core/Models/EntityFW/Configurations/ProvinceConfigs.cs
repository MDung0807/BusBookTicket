using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class ProvinceConfigs : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        #region -- Properties --

        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.AdministrativeRegion)
            .WithMany(x => x.Provinces)
            .HasForeignKey("administrativeRegionId");

        builder.HasOne(x => x.AdministrativeUnit)
            .WithMany(x => x.Provinces)
            .HasForeignKey("administrativeUnitId");

        #endregion -- Relationship --
    }
}