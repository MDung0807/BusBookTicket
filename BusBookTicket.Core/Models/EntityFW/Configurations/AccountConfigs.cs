using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class AccountConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasIndex(x => x.Username);
            builder.Property(x => x.Password)
                .IsRequired();
            builder.HasAlternateKey(x => x.Username);

            #region -- Relationship --
            builder.HasOne(x => x.Role)
                .WithMany(x => x.Accounts)
                .HasForeignKey("RoleID")
                .IsRequired();
            #endregion -- Relationship --


        }
    }
}
