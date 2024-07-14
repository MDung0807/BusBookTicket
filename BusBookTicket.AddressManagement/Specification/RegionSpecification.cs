using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Specification;

public sealed class RegionSpecification : BaseSpecification<AdministrativeRegion>
{
    public RegionSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Provinces);
        
        ApplyOrderBy(x => x.Name);

    }

    public RegionSpecification()
    {
        
    }
}