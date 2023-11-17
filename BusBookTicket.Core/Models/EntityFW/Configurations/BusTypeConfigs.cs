using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class BusTypeConfigs : BaseEntityConfigs, IEntityTypeConfiguration<BusType>
    {
        public void Configure(EntityTypeBuilder<BusType> builder)
        {

            #region -- Properties --

            builder.HasAlternateKey(x => x.Name);
            builder.Property(x => x.Name).IsRequired();

            #endregion -- Properties --

            #region -- relationship --

            #endregion -- relationship --
        }
    }
}
