using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.Unit;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.Unit;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.AddressManagement.Services.UnitService;

public class UnitService : IUnitService
{
    #region -- Properties --

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<AdministrativeUnit> _repository;
    private readonly IMapper _mapper;

    #endregion

    public UnitService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<AdministrativeUnit>();
        _mapper = mapper;
    }
    
    #region -- Public Method --

    public async Task<UnitResponse> GetById(int id)
    {
        UnitSpecification unitSpecification = new UnitSpecification(id);
        AdministrativeUnit unit = await _repository.Get(unitSpecification);
        UnitResponse response = new UnitResponse();
        response.FullName = unit.FullName;
        response.Provinces = await AppUtils.MappObject<Province, ProvinceResponse>(unit.Provinces.ToList(), _mapper);
        return response;
    }

    public async Task<List<UnitResponse>> GetAll()
    {
        UnitSpecification unitSpecification = new UnitSpecification();
        List<AdministrativeUnit> units = await _repository.ToList(unitSpecification);
        return await AppUtils.MappObject<AdministrativeUnit, UnitResponse>(units, _mapper);
    }

    public Task<bool> Update(UnitUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(UnitCreate entity, int userId)
    {
        throw new NotImplementedException();
    }

    #endregion
}