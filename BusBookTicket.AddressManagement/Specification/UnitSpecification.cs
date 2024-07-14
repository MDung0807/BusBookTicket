using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Specification;

public sealed class UnitSpecification : BaseSpecification<AdministrativeUnit>
{
    public UnitSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Provinces);
        ApplyOrderBy(x => x.FullName);
    }

    public UnitSpecification()
    {
        
    }
}