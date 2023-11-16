using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BillItemConfigs :BaseEntityConfigs, IEntityTypeConfiguration<BillItem>
    {
        public void Configure(EntityTypeBuilder<BillItem> builder)
        {

            #region -- Properties --
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.Bill)
                .WithMany(x => x.BillItems)
                .HasForeignKey("BillID")
                .IsRequired();

            builder.HasOne(x => x.TicketItem)
                .WithOne(x => x.BillItem)
                .HasForeignKey<BillItem>("TicketItemID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
