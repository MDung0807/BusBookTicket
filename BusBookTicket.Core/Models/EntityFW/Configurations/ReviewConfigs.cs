using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class ReviewConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            #region -- Properties --
            builder.Property(x => x.Rate)
                .IsRequired();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Reviews)
                .HasForeignKey("CustomerID")
                .IsRequired();

            builder.HasOne(x => x.Bus)
                .WithMany(x => x.Reviews)
                .HasForeignKey("BusID")
                .IsRequired();
            #endregion -- Relationship --

        }
    }
}
