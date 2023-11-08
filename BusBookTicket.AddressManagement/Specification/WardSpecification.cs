using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Specification;

public sealed class WardSpecification : BaseSpecification<Ward>
{
    public WardSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.District);
        AddInclude(x => x.AdministrativeUnit);
        AddInclude(x => x.District.Province);
        AddInclude(x => x.District.Province.AdministrativeRegion);
    }
}