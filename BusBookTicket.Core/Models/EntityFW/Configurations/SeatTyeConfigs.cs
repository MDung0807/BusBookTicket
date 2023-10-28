using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class SeatTyeConfigs : IEntityTypeConfiguration<SeatType>
{
    public void Configure(EntityTypeBuilder<SeatType> builder)
    {
        #region -- Properties --

        builder.HasKey(x => x.typeID);

        builder.Property(x => x.typeID).ValueGeneratedOnAdd();

        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.Company)
            .WithMany(x => x.SeatTypes)
            .HasForeignKey("companyID")
            .IsRequired(false);

        #endregion -- Relationship --
    }
}