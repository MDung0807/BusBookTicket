using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class Ticket_RouteDetailConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Ticket_RouteDetail>
{
    public void Configure(EntityTypeBuilder<Ticket_RouteDetail> builder)
    {
        builder.Property(x => x.DepartureTime).IsRequired(false);
        builder.Property(x => x.ArrivalTime).IsRequired(false);
        builder.HasOne(x => x.Ticket)
            .WithMany(x => x.TicketRouteDetails)
            .HasForeignKey("TicketId");
        
        builder.HasOne(x => x.RouteDetail)
            .WithMany(x => x.TicketRouteDetails)
            .HasForeignKey("RouteDetailId");
    }
}