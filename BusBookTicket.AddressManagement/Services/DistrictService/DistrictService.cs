using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.District;
using BusBookTicket.AddressManagement.DTOs.Responses.District;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Application.Paging;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;

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
        District district = await _repository.Get(districtSpecification, checkStatus: false);
        DistrictResponse response = new DistrictResponse();

        response = _mapper.Map<DistrictResponse>(district);
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

    public Task<PagingResult<DistrictResponse>> GetAllByAdmin(PagingRequest pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PagingResult<DistrictResponse>> GetAll(PagingRequest pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PagingResult<DistrictResponse>> GetAll(PagingRequest pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PagingResult<DistrictResponse>> FindByParam(string param, PagingRequest pagingRequest, bool checkStatus = true)
    {
        throw new NotImplementedException();
    }

    public Task<List<DistrictResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }

    #endregion -- Public Method --

    public Task<List<DistrictResponse>> GetDistrictByUnit(int provinceId)
    {
        throw new NotImplementedException();
    }
}