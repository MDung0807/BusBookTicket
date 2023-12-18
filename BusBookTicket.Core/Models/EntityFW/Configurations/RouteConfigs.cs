using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusBookTicket.Core.Models.EntityFW.Configurations;

public class RouteConfigs : BaseEntityConfigs, IEntityTypeConfiguration<Routes>
{
    public void Configure(EntityTypeBuilder<Routes> builder)
    {
        
    }
}