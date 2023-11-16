using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.District;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.AddressManagement.Services.DistrictService;

public class DistrictService : IDistrictService
{
    #region -- Properties --

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<District> _repository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public DistrictService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.GenericRepository<District>();
    }

    #region -- Public Method --
    public async Task<DistrictResponse> GetById(int id)
    {
        DistrictSpecification districtSpecification = new DistrictSpecification(id);
        District district = await _repository.Get(districtSpecification);
        DistrictResponse response = new DistrictResponse();
        response.FullName = district.FullName;
        response.Wards = await AppUtils.MappObject<Ward, WardResponse>(district.Wards.ToList(), _mapper);
        return response;
    }

    public Task<List<DistrictResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(DistrictUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(DistrictCreate entity, int userId)
    {
        throw new NotImplementedException();
    }
    #endregion -- Public Method --

    public Task<List<DistrictResponse>> GetDistrictByUnit(int provinceId)
    {
        throw new NotImplementedException();
    }
}