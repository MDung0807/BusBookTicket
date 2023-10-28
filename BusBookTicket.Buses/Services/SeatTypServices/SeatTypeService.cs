using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.SeatTypeRepositories;
using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.SeatTypServices;

public class SeatTypeService : ISeatTypeService
{
    #region -- Properties --

    private readonly ISeatTypeRepos _seatTypeResponse;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public SeatTypeService(ISeatTypeRepos repos, IMapper mapper)
    {
        this._mapper = mapper;
        this._seatTypeResponse = repos;
    }
    public async Task<SeatTypeResponse> getByID(int id)
    {
        SeatType seatType = await _seatTypeResponse.getByID(id);
        return _mapper.Map<SeatTypeResponse>(seatType);
    }

    public Task<List<SeatTypeResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> update(SeatTypeFormUpdate entity, int id)
    {
        SeatType seatType = _mapper.Map<SeatType>(entity);
        await _seatTypeResponse.update(seatType);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        SeatType seatType = await _seatTypeResponse.getByID(id);
        seatType.status = (int)EnumsApp.Delete;
        await _seatTypeResponse.delete(seatType);
        return true;
    }

    public async Task<bool> create(SeatTypeFormCreate entity)
    {
        SeatType seatType = _mapper.Map<SeatType>(entity);
        await _seatTypeResponse.create(seatType);
        return true;
    }

    public async Task<List<SeatTypeResponse>> getAll(int companyID)
    {
        List<SeatType> seatTypes = await _seatTypeResponse.getAll(companyID);
        List<SeatTypeResponse> responses = await AppUtils.MappObject<SeatType, SeatTypeResponse>(seatTypes, _mapper);
        return responses;
    }
}