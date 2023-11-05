using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class DiscountConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {

            #region -- Properties --

            builder.Property(x => x.DateStart)
                .IsRequired();
            builder.Property(x => x.DateEnd)
                .IsRequired();
            #endregion -- Properties --

            #region -- relationship --
            builder.HasOne(x => x.Rank)
               .WithMany(x => x.Discounts)
               .HasForeignKey("rankID")
               .IsRequired(false);
            #endregion -- relationship --
        }
    }
}
