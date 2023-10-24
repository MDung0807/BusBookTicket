using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class CustomerConfigs : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            #region -- configs property --

            builder.HasKey(x => x.customerID);
            builder.Property(x => x.customerID)
                .ValueGeneratedOnAdd()
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

            builder.HasIndex(x => x.customerID);
            #endregion -- configs property --

            #region -- RelationShip--
            builder.HasOne(x => x.account)
                .WithOne(x => x.customer)
                .HasForeignKey<Customer>("accountID")
                .IsRequired();

            builder.HasOne(x => x.rank)
                .WithMany(x => x.customers)
                .HasForeignKey("rankID")
                .IsRequired(false);
            #endregion -- RelationShip --
        }
    }
}
