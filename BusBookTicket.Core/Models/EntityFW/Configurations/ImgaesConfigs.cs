using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class ImgaesConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Images>
{
    public void Configure(EntityTypeBuilder<Images> builder)
    {
        #region -- Properties --
        builder.Property(x => x.Image).IsRequired();
        builder.Property(x => x.Id01).IsRequired();
        builder.Property(x => x.ObjectModel).IsRequired();

        #endregion
    }
}