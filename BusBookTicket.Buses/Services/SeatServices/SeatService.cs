using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.SeatRepositories;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.SeatServices;

public class SeatService : ISeatService
{
    #region -- Properties --

    private readonly ISeatRepository _seatRepository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public SeatService(ISeatRepository repository, IMapper mapper)
    {
        this._seatRepository = repository;
        this._mapper = mapper;
    }
    
    public async Task<SeatResponse> getByID(int id)
    {
        Seat seat = await _seatRepository.getByID(id);
        return _mapper.Map<SeatResponse>(seat);
    }

    public Task<List<SeatResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> update(SeatForm entity, int id)
    {
        Seat seat = _mapper.Map<Seat>(entity);
        await _seatRepository.update(seat);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Seat seat = await _seatRepository.getByID(id);
        seat.status = (int)EnumsApp.Delete;
        await _seatRepository.delete(seat);
        return true;
    }

    public async Task<bool> create(SeatForm entity)
    {
        Seat seat = _mapper.Map<Seat>(entity);
        await _seatRepository.create(seat);
        return true;
    }

    public async Task<List<SeatResponse>> getSeatInBus(int busID)
    {
        List<Seat> seats = await _seatRepository.getSeatInBus(busID);
        List<SeatResponse> responses = await AppUtils.MappObject<Seat, SeatResponse>(seats, _mapper);
        return responses;
    }
}