using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.BusType;
using BusBookTicket.Buses.Repository;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public class BusTypeService : IBusTypeService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<BusType> _repository;
    private readonly IBusTypeRepository _busTypeRepository;
    #endregion -- Properties --

    public BusTypeService(IMapper mapper, IUnitOfWork unitOfWork, IBusTypeRepository busTypeRepository)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        _busTypeRepository = busTypeRepository;
        this._repository = unitOfWork.GenericRepository<BusType>();
    }
    public async Task<BusTypeResponse> GetById(int id)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id);
        BusType busType = await _repository.Get(busTypeSpecification);
        return _mapper.Map<BusTypeResponse>(busType);
    }

    public async Task<List<BusTypeResponse>> GetAll()
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification();
        List<BusType> busTypes = await _repository.ToList(busTypeSpecification);
        List<BusTypeResponse> responses = await AppUtils.MapObject<BusType, BusTypeResponse>(busTypes, _mapper);
        return responses;
    }

    public async Task<bool> Update(BusTypeFormUpdate entity, int id, int userId)
    {
        BusType busType = _mapper.Map<BusType>(entity);
        busType.Id = id;
        await _repository.Update(busType, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id);
        BusType busType = await _repository.Get(busTypeSpecification, checkStatus: false);
        busType.Status = (int)EnumsApp.Delete;
        await _repository.Delete(busType, userId);
        return true;
    }

    public async Task<bool> Create(BusTypeForm entity, int userId)
    {
        BusType busType = _mapper.Map<BusType>(entity);
        await _repository.Create(busType, userId);
        return true;
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification, checkStatus: false);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Active);
    }

    public async Task<bool> ChangeIsLock(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification, checkStatus: false);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Lock);
    }

    public async Task<bool> ChangeToWaiting(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification, checkStatus: false);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Waiting);
    }

    public async Task<bool> ChangeToWaiting(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatus(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeToDisable(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification, checkStatus: false);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Disable);
    }

    public Task<bool> CheckToExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    public Task<BusTypePagingResult> GetAllByAdmin(BusTypePaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<BusTypePagingResult> GetAll(BusTypePaging pagingRequest)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(pagingRequest);
        List<BusType> busTypes = await _repository.ToList(busTypeSpecification);
        int count = await _repository.Count(new BusTypeSpecification());
        List<BusTypeResponse> responses = await AppUtils.MapObject<BusType, BusTypeResponse>(busTypes, _mapper);
        BusTypePagingResult result = AppUtils.ResultPaging<BusTypePagingResult, BusTypeResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count,
            responses);
        return result;
    }

    public Task<BusTypePagingResult> GetAll(BusTypePaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BusTypePagingResult> FindByParam(string param, BusTypePaging pagingRequest = default, bool checkStatus = true)
    {
        BusTypeSpecification specification = new BusTypeSpecification(),
            specificationNotPaging = new BusTypeSpecification();
        specification.FindByParam(param, pagingRequest, checkStatus);
        specificationNotPaging.FindByParam(param,checkStatus: checkStatus);
        var result = await _repository.ToList(specification);
        int count = await _repository.Count(specificationNotPaging);
        return AppUtils.ResultPaging<BusTypePagingResult, BusTypeResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count,
            await AppUtils.MapObject<BusType, BusTypeResponse>(result, _mapper));
    }

    public async Task<List<BusTypeResponse>> Statistical(int idMaster, bool checkStatus)
    {
        BusTypeSpecification specification = new BusTypeSpecification();
        specification.Statistical(idMaster: idMaster, checksStatus: true);
        List<BusType> busTypes = await _repository.ToList(specification: specification);

        List<BusTypeResponse> responses = new List<BusTypeResponse>();
        BusTypeResponse busTypeResponse = new BusTypeResponse();
        foreach (var busType in busTypes)
        {
            busTypeResponse = _mapper.Map<BusTypeResponse>(busType);
            if (busType.Buses != null) busTypeResponse.TotalBus = busType.Buses.Count();
            responses.Add(busTypeResponse);
        }

        return responses;
    }

    public async Task<List<object>> Statistical()
    {
        return await _busTypeRepository.TotalBusInType();
    }

    public Task<List<BusTypeResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
}