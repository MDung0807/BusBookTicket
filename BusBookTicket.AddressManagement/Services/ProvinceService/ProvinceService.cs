using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
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
        Province province = await _repository.Get(provinceSpecification, checkStatus: false);
        ProvinceResponse response = new ProvinceResponse();
        response = _mapper.Map<ProvinceResponse>(province);
        return response;
    }

    public async Task<List<ProvinceResponse>> GetAll()
    {
        ProvinceSpecification provinceSpecification = new ProvinceSpecification();
        List<Province> provinces = await _repository.ToList(provinceSpecification);
        List<ProvinceResponse> responses = await AppUtils.MapObject<Province, ProvinceResponse>(provinces, _mapper);
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

    public Task<bool> ChangeToWaiting(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeStatus(List<int> ids, int userId)
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

    public Task<object> GetAll(object pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<object> FindByParam(string param, object pagingRequest, bool checkStatus = true)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProvinceResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
}