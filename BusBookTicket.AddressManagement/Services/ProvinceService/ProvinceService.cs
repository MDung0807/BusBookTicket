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
        response = _mapper.Map<ProvinceResponse>(province);
        return response;
    }

    public async Task<List<ProvinceResponse>> GetAll()
    {
        ProvinceSpecification provinceSpecification = new ProvinceSpecification();
        List<Province> provinces = await _repository.ToList(provinceSpecification);
        List<ProvinceResponse> responses = await AppUtils.MappObject<Province, ProvinceResponse>(provinces, _mapper);
        return responses;
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
}