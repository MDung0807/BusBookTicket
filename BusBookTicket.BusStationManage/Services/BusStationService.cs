using AutoMapper;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Repositories;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.BusStationManage.Services;

public class BusStationService : IBusStationService
{
    #region -- Properties --

    private readonly IBusStationRepos _busStationRepos;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    #region -- Public Method --
    public BusStationService(IBusStationRepos busStationRepos, IMapper mapper)
    {
        this._busStationRepos = busStationRepos;
        this._mapper = mapper;
    }
    public async Task<BusStationResponse> getByID(int id)
    {
        BusStation busStation = await _busStationRepos.getByID(id);
        return _mapper.Map<BusStationResponse>(busStation);
    }

    public async Task<List<BusStationResponse>> getAll()
    {
        List<BusStationResponse> responses = new List<BusStationResponse>();
        List<BusStation> busStations = await _busStationRepos.getAll();
        responses = await AppUtils.MappObject<BusStation, BusStationResponse>(busStations, _mapper);
        return responses;
    }

    public async Task<bool> update(BST_FormUpdate entity, int id)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        busStation.busStationID = id;
        await _busStationRepos.update(busStation);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        BusStation busStation = await _busStationRepos.getByID(id);
        busStation.status = (int)EnumsApp.Delete;
        await _busStationRepos.delete(busStation);
        return true;
    }

    public async Task<bool> create(BST_FormCreate entity)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        await _busStationRepos.create(busStation);
        return true;
    }

    public async Task<BusStationResponse> getStationByName(string name)
    {
        BusStation busStation = await _busStationRepos.getStationByName(name);
        return _mapper.Map<BusStationResponse>(busStation);
    }

    public async Task<List<BusStationResponse>> getStationByLocaion(string location)
    {
        List<BusStation> busStations = await _busStationRepos.getAllStationByLocation(location);
        List<BusStationResponse> responses = await AppUtils.MappObject<BusStation, BusStationResponse>(busStations, _mapper);

        return responses;
    }

    #endregion
}