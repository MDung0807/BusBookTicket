using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.SeatType;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.SeatTypServices;

public class SeatTypeService : ISeatTypeService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<SeatType> _repository;
    #endregion -- Properties --

    public SeatTypeService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<SeatType>();
    }
    public async Task<SeatTypeResponse> GetById(int id)
    {
        SeatTypeSpecification seatTypeSpecification = new SeatTypeSpecification(id);
        SeatType seatType = await _repository.Get(seatTypeSpecification);
        return _mapper.Map<SeatTypeResponse>(seatType);
    }

    public Task<List<SeatTypeResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(SeatTypeFormUpdate entity, int id, int userId)
    {
        SeatType seatType = _mapper.Map<SeatType>(entity);
        await _repository.Update(seatType, userId); 
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        SeatTypeSpecification seatTypeSpecification = 
            new SeatTypeSpecification(id, userId: userId, false, getIsChangeStatus:true);
        SeatType seatType = await _repository.Get(seatTypeSpecification, checkStatus: false);
        seatType.Status = (int)EnumsApp.Delete;
        await _repository.Delete(seatType, userId);
        return true;
    }

    public async Task<bool> Create(SeatTypeFormCreate entity, int userId)
    {
        SeatType seatType = _mapper.Map<SeatType>(entity);
        seatType.Status = (int)EnumsApp.Active;
        await _repository.Create(seatType, userId);
        return true;
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        SeatTypeSpecification seatTypeSpecification = 
            new SeatTypeSpecification(id, userId: userId, false, getIsChangeStatus:true);
        SeatType seatType = await _repository.Get(seatTypeSpecification, checkStatus: false);
        await _repository.ChangeStatus(seatType, userId: userId, (int)EnumsApp.Active);
        return true;
    }

    public Task<bool> ChangeIsLock(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeToWaiting(int id, int userId)
    {
        throw new NotImplementedException();
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
        SeatTypeSpecification seatTypeSpecification = 
            new SeatTypeSpecification(id, userId: userId, false, getIsChangeStatus:true);
        SeatType seatType = await _repository.Get(seatTypeSpecification, checkStatus: false);
        await _repository.ChangeStatus(seatType, userId: userId, (int)EnumsApp.Disable);
        return true;
    }

    public Task<bool> CheckToExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    public Task<SeatTypePagingResult> GetAllByAdmin(SeatTypePaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<SeatTypePagingResult> GetAll(SeatTypePaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<SeatTypePagingResult> GetAll(SeatTypePaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        SeatTypeSpecification seatTypeSpecification = new SeatTypeSpecification(0, idMaster, paging: pagingRequest, checkStatus: checkStatus);   
        List<SeatType> seatTypes = await _repository.ToList(seatTypeSpecification);
        int count = await _repository.Count(new SeatTypeSpecification(0, companyId: idMaster, checkStatus: checkStatus));
        List<SeatTypeResponse> responses = await AppUtils.MapObject<SeatType, SeatTypeResponse>(seatTypes, _mapper);
        SeatTypePagingResult result = AppUtils.ResultPaging<SeatTypePagingResult, SeatTypeResponse>(
            pagingRequest.PageIndex, pagingRequest.PageSize,
            count:count, responses);
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<SeatTypePagingResult> FindByParam(string param, SeatTypePaging pagingRequest = default, bool checkStatus = true)
    {
        SeatTypeSpecification specification = new SeatTypeSpecification(),
            specificationNotPaging = new SeatTypeSpecification();
        specification.FindByParam(param, pagingRequest, checkStatus);
        specificationNotPaging.FindByParam(param, checkStatus:checkStatus);
        var result = await _repository.ToList(specification);
        int count = await _repository.Count(specificationNotPaging);
        
        return AppUtils.ResultPaging<SeatTypePagingResult, SeatTypeResponse>(
            pagingRequest.PageIndex, pagingRequest.PageSize,
            count:count, await AppUtils.MapObject<SeatType, SeatTypeResponse>(result, _mapper));
    }


    public Task<List<SeatTypeResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
    
}