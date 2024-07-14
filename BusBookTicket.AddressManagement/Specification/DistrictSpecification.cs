using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Specification;

public sealed class DistrictSpecification : BaseSpecification<District>
{
    public DistrictSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Wards);
        AddInclude(x => x.Province);
        AddInclude(x => x.Province.AdministrativeUnit);
        AddInclude(x => x.Province.AdministrativeRegion);
        
        ApplyOrderBy(x => x.FullName);

    }
    
    public DistrictSpecification(int id, int idProvince) : base(x => x.Province.Id == id)
    {
        AddInclude(x => x.Province);
        AddInclude(x => x.AdministrativeUnit);
        
        ApplyOrderBy(x => x.FullName);
    }
}