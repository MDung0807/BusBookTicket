using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class NotificationChangeConfigs : BaseEntityConfigs, IEntityTypeConfiguration<NotificationChange>
{
    public void Configure(EntityTypeBuilder<NotificationChange> builder)
    {
        #region --Relationship --

        builder.HasOne(x => x.ActorCompany)
            .WithMany(x => x.NotificationChanges)
            .HasForeignKey("ActorCompanyId");
        
        builder.HasOne(x => x.ActorCustomer)
            .WithMany(x => x.NotificationChanges)
            .HasForeignKey("ActorCustomerId");
        
        builder.HasOne(x => x.NotificationObject)
            .WithMany(x => x.NotificationChanges)
            .HasForeignKey("NotificationObjectId");
        #endregion --Relationship --
    }
}