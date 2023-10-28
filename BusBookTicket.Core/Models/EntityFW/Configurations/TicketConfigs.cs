using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class TicketConfigs : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        #region -- Properties --
        builder.HasKey(x => x.ticketID);

        builder.Property(x => x.ticketID)
            .ValueGeneratedOnAdd();
        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.bus)
            .WithMany(x => x.tickets)
            .HasForeignKey("busID")
            .IsRequired();

        #endregion -- Relationship --
    }
}