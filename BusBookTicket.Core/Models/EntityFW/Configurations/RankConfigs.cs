using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations
{
    public class RankConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {

            #region -- Properties --

            #endregion -- Properties --

            #region -- Relationship --
            #endregion -- Relationship --
        }
    }
}
