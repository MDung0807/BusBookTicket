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
            .HasForeignKey("busID")
            .IsRequired();

        builder.HasOne(x => x.SeatType)
            .WithMany(x => x.Seats)
            .HasForeignKey("seatTypeID")
            .IsRequired();

        #endregion -- Relationship --
    }
}