using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class SeatConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        #region -- Properties --
        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.Bus)
            .WithMany(x => x.Seats)
            .HasForeignKey("BusID")
            .IsRequired();

        builder.HasOne(x => x.SeatType)
            .WithMany(x => x.Seats)
            .HasForeignKey("SeatTypeID")
            .IsRequired();

        #endregion -- Relationship --
    }
}