using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW.Configurations;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.AddressManagement.Services.ProvinceService;

public class ProvinceService : IProvinceService
{
    #region -- Properties --

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Province> _repository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public ProvinceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.GenericRepository<Province>();
    }
    public async Task<ProvinceResponse> GetById(int id)
    {
        ProvinceSpecification provinceSpecification = new ProvinceSpecification(id);
        Province province = await _repository.Get(provinceSpecification);
        ProvinceResponse response = new ProvinceResponse();
        response.FullName = province.FullName;
        response.Districts = await AppUtils.MappObject<District, DistrictResponse>(province.Districts.ToList(), _mapper);
        return response;
    }

    public Task<List<ProvinceResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(ProvinceUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(ProvinceCreate entity, int userId)
    {
        throw new NotImplementedException();
    }
}