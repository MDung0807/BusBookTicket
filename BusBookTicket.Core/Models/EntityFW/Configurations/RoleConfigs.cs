using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class RoleConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            #region -- Properties --
            builder.HasAlternateKey(x => x.RoleName);
            #endregion -- Properties --

            #region -- Relationship --
            
            #endregion -- Relationship --
        }

    }
}
