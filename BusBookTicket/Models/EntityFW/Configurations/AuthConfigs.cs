using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Models.EntityFW.Configurations
{
    public class AccountConfigs : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.accountID);

            builder.Property(x => x.accountID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.username)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.password)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.customer)
                .WithOne(b => b.account).
                HasForeignKey<Customer>(x=> x.accountID);

            builder.HasOne(x => x.company)
                .WithOne(builder => builder.account)
                .HasForeignKey<Company>(x=> x.companyID);
        }
    }
}
