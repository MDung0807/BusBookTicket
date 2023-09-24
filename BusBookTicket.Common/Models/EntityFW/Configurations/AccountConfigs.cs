using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusBookTicket.Common.Models.EntityFW.Configurations
{
    public class AccountConfigs : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.accountID);

            builder.Property(x => x.accountID)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.username)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.password)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasAlternateKey(x => x.accountID);
            
        }
    }
}
