using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class SeatConfigs : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        #region -- Properties --
        builder.HasKey(x => x.seatID);

        builder.Property(x => x.seatID)
            .ValueGeneratedOnAdd();
        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.bus)
            .WithMany(x => x.seats)
            .HasForeignKey("busID")
            .IsRequired();

        #endregion -- Relationship --
    }
}