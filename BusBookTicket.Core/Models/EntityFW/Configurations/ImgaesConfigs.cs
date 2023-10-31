using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class ImgaesConfigs : IEntityTypeConfiguration<Images>
{
    public void Configure(EntityTypeBuilder<Images> builder)
    {
        #region -- Properties --
        builder.HasKey(x => x.id);

        builder.Property(x => x.id).HasDefaultValueSql("NEWID()");
        builder.Property(x => x.image).IsRequired();
        builder.Property(x => x.id01).IsRequired();
        builder.Property(x => x.objectModel).IsRequired();

        #endregion
    }
}