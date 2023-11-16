using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class CompanyConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {

            #region -- Properties --

            builder.Property(x => x.Email)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .IsRequired();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.Account)
                .WithOne(x => x.Company)
                .HasForeignKey<Company>("AccountID")
                .IsRequired();
            
            
            builder.HasOne(x => x.Ward)
                .WithMany(x => x.Companies)
                .HasForeignKey("WardId")
                .IsRequired(false);
            #endregion -- Relationship --
        }
    }
}
