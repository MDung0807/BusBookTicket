using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.BusType;
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
    #endregion -- Properties --

    public BusTypeService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
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
        BusType busType = await _repository.Get(busTypeSpecification);
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
        BusType busType = await _repository.Get(busTypeSpecification);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Active);
    }

    public async Task<bool> ChangeIsLock(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Lock);
    }

    public async Task<bool> ChangeToWaiting(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification);
        return await _repository.ChangeStatus(busType, userId, (int)EnumsApp.Waiting);
    }

    public async Task<bool> ChangeToDisable(int id, int userId)
    {
        BusTypeSpecification busTypeSpecification = new BusTypeSpecification(id, false);
        BusType busType = await _repository.Get(busTypeSpecification);
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

    public Task<BusTypePagingResult> GetAll(BusTypePaging pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BusTypeResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
}