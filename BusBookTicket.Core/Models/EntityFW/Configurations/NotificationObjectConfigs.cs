using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class NotificationObjectConfigs : BaseEntityConfigs, IEntityTypeConfiguration<NotificationObject>
{
    public void Configure(EntityTypeBuilder<NotificationObject> builder)
    {
        #region -- Relationship--

        builder.HasOne(x => x.Event)
            .WithMany(x => x.NotificationObject)
            .HasForeignKey("EventId");

        #endregion -- Relationship--
    }
}