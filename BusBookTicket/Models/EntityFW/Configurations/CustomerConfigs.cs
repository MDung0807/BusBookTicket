using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Models.EntityFW.Configurations
{
    public class CustomerConfigs : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            throw new NotImplementedException();
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
                .HasMaxLength(50);
            builder.Property(x => x.phoneNumber)
                .HasMaxLength(50); 
            builder.Property(x => x.gender)
                .HasMaxLength(50); 
            builder.Property(x => x.dateCreate)
                .HasMaxLength(50); 
            builder.Property(x => x.dateUpdate)
                .HasMaxLength(50); 
            #endregion -- configs property --

            #region -- RelationShip--
            builder.HasMany(x => x.reviews)
                .WithOne(b => b.customer)
                .HasForeignKey(b => b.customerID);

            builder.HasMany(x => x.tickets)
                .WithOne(b => b.customer)
                .HasForeignKey(b => b.customerID);
            #endregion -- RelationShip --
        }
    }
}
