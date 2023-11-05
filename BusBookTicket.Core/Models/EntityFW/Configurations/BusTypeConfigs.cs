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

            #endregion -- Properties --

            #region -- relationship --
            
            #endregion -- relationship --
        }
    }
}
