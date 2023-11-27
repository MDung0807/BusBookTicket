using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class Ticket_BusStopConfigs : IEntityTypeConfiguration<Ticket_BusStop>
{
    public void Configure(EntityTypeBuilder<Ticket_BusStop> builder)
    {
        #region -- Properties

        

        #endregion -- Properties

        #region -- Relationship --

        builder.HasOne(x => x.Ticket)
            .WithMany(x => x.TicketBusStops)
            .HasForeignKey("TicketId");

        builder.HasOne(x => x.BusStop)
            .WithMany(x => x.TicketBusStops)
            .HasForeignKey("BusStopId");

        #endregion -- Relationship --
    }
}