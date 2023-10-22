using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class CompanyConfigs : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {

            #region -- Properties --

            builder.HasKey(x => x.companyID);

            builder.Property(x => x.email)
                .IsRequired();
            builder.Property(x => x.phoneNumber)
                .IsRequired();
            #endregion -- Properties --

            #region -- Relationship --
            builder.HasOne(x => x.account)
                .WithOne(x => x.company)
                .HasForeignKey<Company>("accountID")
                .IsRequired();
            #endregion -- Relationship --
        }
    }
}
