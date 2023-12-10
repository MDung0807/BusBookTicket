using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.Region;
using BusBookTicket.AddressManagement.DTOs.Responses.Region;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.AddressManagement.Services.RegionService;

public class RegionService : IRegionService
{
    #region -- Properties --

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<AdministrativeRegion> _repository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public RegionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.GenericRepository<AdministrativeRegion>();
    }

    #region -- Public Method --

    public async Task<RegionResponse> GetById(int id)
    {
        RegionSpecification regionSpecification = new RegionSpecification(id);
        AdministrativeRegion region = await _repository.Get(regionSpecification);
        RegionResponse response = new RegionResponse();
        response = _mapper.Map<RegionResponse>(region);
        return response;
    }

    public async Task<List<RegionResponse>> GetAll()
    {
        RegionSpecification regionSpecification = new RegionSpecification();
        List<AdministrativeRegion> regions = await _repository.ToList(regionSpecification);
        return await AppUtils.MapObject<AdministrativeRegion, RegionResponse>(regions, _mapper);
    }

    public Task<bool> Update(RegionUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(RegionCreate entity, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsActive(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsLock(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeToWaiting(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeToDisable(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAllByAdmin(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
    }

    public Task<List<RegionResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }

    #endregion
    
}