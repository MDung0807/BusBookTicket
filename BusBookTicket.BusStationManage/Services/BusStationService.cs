using AutoMapper;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Repositories;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;

namespace BusBookTicket.BusStationManage.Services;

public class BusStationService : IBusStationService
{
    #region -- Properties --

    private readonly IBusStationRepos _busStationRepos;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public BusStationService(IBusStationRepos busStationRepos, IMapper mapper)
    {
        this._busStationRepos = busStationRepos;
        this._mapper = mapper;
    }
    public BusStationResponse getByID(int id)
    {
        BusStation busStation = _busStationRepos.getByID(id);
        return _mapper.Map<BusStationResponse>(busStation);
    }

    public List<BusStationResponse> getAll()
    {
        List<BusStationResponse> responses = new List<BusStationResponse>();
        List<BusStation> busStations = _busStationRepos.getAll();
        responses = AppUtils.MappObject<BusStation, BusStationResponse>(busStations, _mapper);
        return responses;
    }

    public bool update(BST_FormUpdate entity, int id)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        busStation.busStationID = id;

        return _busStationRepos.update(busStation);
    }

    public bool delete(int id)
    {
        return _busStationRepos.delete(id);
    }

    public bool create(BST_FormCreate entity)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        return _busStationRepos.create(busStation);
    }
}