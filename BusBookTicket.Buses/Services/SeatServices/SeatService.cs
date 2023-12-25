using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Seat;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.SeatServices;

public class SeatService : ISeatService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Seat> _repository;
    #endregion -- Properties --

    public SeatService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Seat>();
        this._mapper = mapper;
    }
    
    public async Task<SeatResponse> GetById(int id)
    {
        SeatSpecification seatSpecification = new SeatSpecification(id);
        Seat seat = await _repository.Get(seatSpecification);
        return _mapper.Map<SeatResponse>(seat);
    }

    public Task<List<SeatResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(SeatForm entity, int id, int userId)
    {
        Seat seat = _mapper.Map<Seat>(entity);
        await _repository.Update(seat, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        SeatSpecification seatSpecification = new SeatSpecification(id);
        Seat seat = await _repository.Get(seatSpecification, checkStatus: false);
        seat.Status = (int)EnumsApp.Delete;
        await _repository.Delete(seat, userId);
        return true;
    }

    public async Task<bool> Create(SeatForm entity, int userId)
    {
        Seat seat = _mapper.Map<Seat>(entity);
        seat.Bus = new Bus();
        seat.Bus.Id = entity.BusId;
        seat.Status = (int)EnumsApp.Active;
        await _repository.Create(seat, userId);
        return true;
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

    public Task<SeatPagingResult> GetAllByAdmin(SeatPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<SeatPagingResult> GetAll(SeatPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<SeatPagingResult> GetAll(SeatPaging pagingRequest, int idMaster)
    {
        SeatSpecification seatSpecification = new SeatSpecification(busId:idMaster, paging: pagingRequest);
        List<Seat> seats = await _repository.ToList(seatSpecification);
        int count = await _repository.Count(new SeatSpecification(busId:idMaster));
        List<SeatResponse> responses = await AppUtils.MapObject<Seat, SeatResponse>(seats, _mapper);
        SeatPagingResult result = AppUtils.ResultPaging<SeatPagingResult, SeatResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count,
            responses);
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<SeatResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
}