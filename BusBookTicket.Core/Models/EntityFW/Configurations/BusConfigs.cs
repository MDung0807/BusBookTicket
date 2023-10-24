using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusConfigs : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            #region -- Properties --
            builder.HasKey(x => x.busID);

            builder.Property(x => x.busID)
                .ValueGeneratedOnAdd()
                .IsRequired() ;
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.busType)
                .WithMany(x => x.buses)
                .HasForeignKey("busTypeID")
                .IsRequired();
            builder.HasOne(x => x.company)
                .WithMany(x => x.buses)
                .HasForeignKey("companyID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
