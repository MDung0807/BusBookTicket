using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Common.Models.EntityFW.Configurations
{
    public class RankConfigs : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {

            #region -- Properties --
            builder.HasKey(x => x.rankID);

            builder.Property(x => x.rankID)
                .ValueGeneratedOnAdd();

            #endregion -- Properties --

            #region -- Relationship --
            #endregion -- Relationship --
        }
    }
}
