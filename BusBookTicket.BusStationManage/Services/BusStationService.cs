using AutoMapper;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.BusStationManage.Services;

public class BusStationService : IBusStationService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<BusStation> _repository;
    #endregion -- Properties --

    #region -- Public Method --
    public BusStationService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<BusStation>();
        this._mapper = mapper;
    }
    public async Task<BusStationResponse> GetById(int id)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return _mapper.Map<BusStationResponse>(busStation);
    }

    public async Task<List<BusStationResponse>> GetAll()
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification();
        List<BusStationResponse> responses = new List<BusStationResponse>();
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        responses = await AppUtils.MappObject<BusStation, BusStationResponse>(busStations, _mapper);
        return responses;
    }

    public async Task<bool> Update(BST_FormUpdate entity, int id, int userId)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        busStation.Id = id;
        await _repository.Update(busStation, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id);
        BusStation busStation = await _repository.Get(busStationSpecification);
        busStation.Status = (int)EnumsApp.Delete;
        await _repository.Update(busStation, userId);
        return true;
    }

    public async Task<bool> Create(BST_FormCreate entity, int userId)
    {
        BusStation busStation = _mapper.Map<BusStation>(entity);
        await _repository.Create(busStation, userId);
        return true;
    }

    public async Task<BusStationResponse> getStationByName(string name)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(name);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return _mapper.Map<BusStationResponse>(busStation);
    }

    public async Task<List<BusStationResponse>> getStationByLocation(string location)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification("", location);
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        List<BusStationResponse> responses = await AppUtils.MappObject<BusStation, BusStationResponse>(busStations, _mapper);

        return responses;
    }

    #endregion
}