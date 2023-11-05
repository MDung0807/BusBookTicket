using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
