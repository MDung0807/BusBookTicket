using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class NotificationConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {

        #region --Relationship --

        builder.HasOne(x => x.NotificationObject)
            .WithMany(x => x.Notifications)
            .HasForeignKey("NotificationId");

        builder.HasOne(x => x.NotifierCompany)
            .WithMany(x => x.Notifications)
            .HasForeignKey("NotifierCompanyId");

        builder.HasOne(x => x.NotifierCustomer)
            .WithMany(x => x.Notifications)
            .HasForeignKey("NotifierCustomerIds");
        #endregion --Relationship --
    }
}