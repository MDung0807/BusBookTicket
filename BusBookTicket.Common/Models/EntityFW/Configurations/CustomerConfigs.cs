using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Common.Models.EntityFW.Configurations
{
    public class CustomerConfigs : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            #region -- configs property --

            builder.HasKey(x => x.CustomerID);
            builder.Property(x => x.CustomerID)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.fullName)
                .HasMaxLength(50);
            builder.Property(x => x.dateOfBirth)
                .HasMaxLength(50);
            builder.Property(x => x.address)
                .HasMaxLength(50);
            builder.Property(x => x.email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.phoneNumber)
                .IsRequired()
                .HasMaxLength(50); 
            builder.Property(x => x.gender)
                .HasMaxLength(50); 
            builder.Property(x => x.dateCreate)
                .HasMaxLength(50); 
            builder.Property(x => x.dateUpdate)
                .HasMaxLength(50);

            builder.HasIndex(x => x.CustomerID);
            #endregion -- configs property --

            #region -- RelationShip--
            builder.HasOne(x => x.account)
                .WithOne(x => x.customer)
                .HasForeignKey<Account>("accountID")
                .IsRequired();

            builder.HasOne(x => x.rank)
                .WithMany(x => x.customers)
                .HasForeignKey("rankID")
                .IsRequired(false);
            #endregion -- RelationShip --
        }
    }
}
