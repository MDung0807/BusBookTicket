using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class DistrictConfigs : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        #region -- Properties --


        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.AdministrativeUnit)
            .WithMany(x => x.Districts)
            .HasForeignKey("AdministrativeUnitId");

        builder.HasOne(x => x.Province)
            .WithMany(x => x.Districts)
            .HasForeignKey("ProvinceId");

        #endregion -- Relationship --
    }
}