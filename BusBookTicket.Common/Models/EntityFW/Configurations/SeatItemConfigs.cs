using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Common.Models.EntityFW.Configurations
{
    public class SeatItemConfigs : IEntityTypeConfiguration<SeatItem>
    {
        public void Configure(EntityTypeBuilder<SeatItem> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.seatID);

            builder.Property(x => x.seatID)
                .ValueGeneratedOnAdd();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.busType)
                .WithMany(x => x.seatItems)
                .HasForeignKey("busTypeID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
