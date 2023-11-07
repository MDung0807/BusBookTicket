using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public abstract class BaseEntityConfigs : IEntityTypeConfiguration<BaseEntity>
{
    public void Configure(EntityTypeBuilder<BaseEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        builder.Property(x => x.DateCreate).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.DateUpdate).HasDefaultValue(DateTime.Now);
        builder.Property(x => x.CreateBy).HasDefaultValue(0);
        builder.Property(x => x.UpdateBy).HasDefaultValue(0);
    }
}