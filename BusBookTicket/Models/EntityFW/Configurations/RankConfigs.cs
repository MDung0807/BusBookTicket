using BusBookTicket.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Models.EntityFW.Configurations
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
