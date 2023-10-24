using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.BusTypeRepositories;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public class BusService : IBusService
{
    #region -- Properties --

    private readonly IBusRepos _busRepos;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public BusService(IBusRepos busRepos, IMapper mapper)
    {
        this._busRepos = busRepos;
        this._mapper = mapper;
    }
    public async Task<BusResponse> getByID(int id)
    {
        Bus bus = await _busRepos.getByID(id);
        return _mapper.Map<BusResponse>(bus);
    }

    public async Task<List<BusResponse>> getAll()
    {
        List<Bus> buses = await _busRepos.getAll();
        List<BusResponse> responses = await AppUtils.MappObject<Bus, BusResponse>(buses, _mapper);
        return responses;
    }

    public async Task<bool> update(FormUpdateBus entity, int id)
    {
        Bus bus = _mapper.Map<Bus>(entity);
        bus.busID = id;
        await _busRepos.update(bus);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Bus bus = await _busRepos.getByID(id);
        bus.status = (int)EnumsApp.Delete;
        await _busRepos.delete(bus);
        return true;
    }

    public async Task<bool> create(FormCreateBus entity)
    {
        Bus bus = _mapper.Map<Bus>(entity);
        await _busRepos.create(bus);
        return true;
    }
}