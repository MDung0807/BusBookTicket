using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class TicketConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        #region -- Properties --
        #endregion -- Properties --

        #region -- Relationship --

        builder.HasOne(x => x.Bus)
            .WithMany(x => x.Tickets)
            .HasForeignKey("busID")
            .IsRequired();

        #endregion -- Relationship --
    }
}