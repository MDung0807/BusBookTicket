using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class CustomerConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            #region -- configs property --

            builder.Property(x => x.FullName)
                .HasMaxLength(50);
            builder.Property(x => x.DateOfBirth)
                .HasMaxLength(50);
            builder.Property(x => x.Address)
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50); 
            builder.Property(x => x.Gender)
                .HasMaxLength(50); 

            #endregion -- configs property --

            #region -- RelationShip--
            builder.HasOne(x => x.Account)
                .WithOne(x => x.Customer)
                .HasForeignKey<Customer>("accountID")
                .IsRequired();

            builder.HasOne(x => x.Rank)
                .WithMany(x => x.Customers)
                .HasForeignKey("rankID")
                .IsRequired(false);
            
            
            builder.HasOne(x => x.Ward)
                .WithMany(x => x.Customers)
                .HasForeignKey("wardId");
            #endregion -- RelationShip --
        }
    }
}
