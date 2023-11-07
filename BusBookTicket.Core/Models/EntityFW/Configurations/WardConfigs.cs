using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class WardConfigs : IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        #region -- Properties --

        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.AdministrativeUnit)
            .WithMany(x => x.Wards)
            .HasForeignKey("administrativeUnitId")
            .IsRequired();

        builder.HasOne(x => x.District)
            .WithMany(x => x.Wards)
            .HasForeignKey("DistrictId")
            .IsRequired();

        #endregion -- Relationship --
    }
}