using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class DiscountConfigs : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {

            #region -- Properties --

            builder.HasKey(x => x.discountID);

            builder.Property(x => x.discountID)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.dateStart)
                .IsRequired();
            builder.Property(x => x.dateEnd)
                .IsRequired();
            builder.Property(x =>x.dateCreate)
                .IsRequired();
            #endregion -- Properties --

            #region -- relationship --
            builder.HasOne(x => x.rank)
               .WithMany(x => x.discounts)
               .HasForeignKey("rankID")
               .IsRequired(false);
            #endregion -- relationship --
        }
    }
}
