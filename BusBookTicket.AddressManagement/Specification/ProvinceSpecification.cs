using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Specification;

public sealed class ProvinceSpecification : BaseSpecification<Province>
{
    public ProvinceSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Districts);
        AddInclude(x => x.AdministrativeUnit);
        AddInclude(x => x.AdministrativeRegion);
    }
    
    public ProvinceSpecification() : base()
    {
        AddInclude(x => x.AdministrativeUnit);
        AddInclude(x => x.AdministrativeRegion);
    }
}