using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Models.EntityFW.Configurations
{
    public class BusTypeConfigs : IEntityTypeConfiguration<BusType>
    {
        public void Configure(EntityTypeBuilder<BusType> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.busTypeID);

            builder.Property(x => x.busTypeID)
                .ValueGeneratedOnAdd();

            #endregion -- Properties --

            #region -- relationship --
            
            #endregion -- relationship --
        }
    }
}
