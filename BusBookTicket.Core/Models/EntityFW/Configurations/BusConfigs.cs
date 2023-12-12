using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            #region -- Properties --

            builder.HasAlternateKey(x => x.BusNumber);
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.BusType)
                .WithMany(x => x.Buses)
                .HasForeignKey("BusTypeID")
                .IsRequired();
            builder.HasOne(x => x.Company)
                .WithMany(x => x.Buses)
                .HasForeignKey("CompanyID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
