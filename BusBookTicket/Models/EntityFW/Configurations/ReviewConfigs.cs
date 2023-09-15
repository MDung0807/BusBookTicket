using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Models.EntityFW.Configurations
{
    public class ReviewConfigs : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.reviewID);

            builder.Property(x => x.reviewID)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.rate)
                .IsRequired();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.customer)
                .WithMany(x => x.reviews)
                .HasForeignKey("customerID");

            builder.HasOne(x => x.company)
                .WithMany(x => x.reviews)
                .HasForeignKey("companyID");
            #endregion -- Relationship --

        }
    }
}
