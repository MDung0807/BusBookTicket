using AutoMapper;
using BusBookTicket.AddressManagement.Services.WardService;
using BusBookTicket.AddressManagement.Utilities;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.BusStationManage.Specification;
using BusBookTicket.BusStationManage.Utils;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.BusStationManage.Services;

public class BusStationService : IBusStationService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IGenericRepository<BusStation> _repository;
    private readonly IWardService _wardService;
    #endregion -- Properties --

    #region -- Public Method --
    public BusStationService(IMapper mapper, IUnitOfWork unitOfWork, IWardService wardService)
    {
        _repository = unitOfWork.GenericRepository<BusStation>();
        this._mapper = mapper;
        _wardService = wardService;
    }
    public async Task<BusStationResponse> GetById(int id)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id);
        BusStation busStation = await _repository.Get(busStationSpecification);
        BusStationResponse response = _mapper.Map<BusStationResponse>(busStation);
        response.AddressDb = await GetFullAddress(response.Address, response.WardId);
        return response;
    }

    public async Task<List<BusStationResponse>> GetAll()
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification();
        List<BusStationResponse> responses = new List<BusStationResponse>();
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        responses = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        for (int i = 0; i < responses.Count; i++)
        {
            responses[i].AddressDb = await GetFullAddress(responses[i].Address, responses[i].WardId);

        }
        return responses;
    }

    public async Task<StationPagingResult> GetAll(StationPaging request)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(paging: request);
        List<BusStationResponse> stationResponses = new List<BusStationResponse>();
        int total = await _repository.Count(busStationSpecification);
        
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        stationResponses = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        StationPagingResult response = AppUtils.ResultPaging<StationPagingResult, BusStationResponse>(
            request.PageIndex, 
            request.PageSize, 
            total,
            stationResponses);

        // response.PageTotal = (int)Math.Round((decimal)total/request.PageSize);
        // response.PageIndex = request.PageIndex;
        // response.PageSize = request.PageSize;
        // response.Items = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        for (int i = 0; i < response.Items.Count; i++)
        {
            response.Items[i].AddressDb = await GetFullAddress(response.Items[i].Address, response.Items[i].WardId);
        }
        return response;
    }

    public Task<StationPagingResult> GetAll(StationPaging pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
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
        BusStationSpecification specification = new BusStationSpecification(entity.Name, false);
        if (await _repository.CheckIsExist(specification))
            throw new ExceptionDetail(BusStationConstants.EXIST_RESOURCE);
        
        BusStation busStation = _mapper.Map<BusStation>(entity);
        await _repository.Create(busStation, userId);
        return true;
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id, false);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return await _repository.ChangeStatus(busStation, userId, (int)EnumsApp.Active);
    }

    public async Task<bool> ChangeIsLock(int id, int userId)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id, false);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return await _repository.ChangeStatus(busStation, userId, (int)EnumsApp.Lock);
    }

    public async Task<bool> ChangeToWaiting(int id, int userId)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id, false);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return await _repository.ChangeStatus(busStation, userId, (int)EnumsApp.Waiting);
    }

    public async Task<bool> ChangeToDisable(int id, int userId)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(id, false);
        BusStation busStation = await _repository.Get(busStationSpecification);
        return await _repository.ChangeStatus(busStation, userId, (int)EnumsApp.Disable);
    }

    public Task<bool> CheckToExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    public async Task<StationPagingResult> GetAllByAdmin(StationPaging pagingRequest)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(false, paging: pagingRequest);
        List<BusStationResponse> responses = new List<BusStationResponse>();
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        responses = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        for (int i = 0; i < responses.Count; i++)
        {
            responses[i].AddressDb = await GetFullAddress(responses[i].Address, responses[i].WardId);
        }

        int count = await _repository.Count(new BusStationSpecification(false));
        StationPagingResult result = new StationPagingResult();
        result.PageIndex = pagingRequest.PageIndex;
        result.PageSize = pagingRequest.PageSize;
        result.PageTotal = (int)Math.Round((decimal)count / pagingRequest.PageSize);
        result.Items = responses;
        return result;
    }

    public async Task<BusStationResponse> GetStationByName(string name)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification(name);
        BusStation busStation = await _repository.Get(busStationSpecification);
        BusStationResponse response =  _mapper.Map<BusStationResponse>(busStation);
        response.AddressDb = await GetFullAddress(response.Address, response.WardId);
        return response;
    }

    public async Task<StationPagingResult> GetStationByLocation(string location, StationPaging pagingRequest)
    {
        BusStationSpecification busStationSpecification = new BusStationSpecification("", location, paging: pagingRequest);
        List<BusStation> busStations = await _repository.ToList(busStationSpecification);
        int count = await _repository.Count(new BusStationSpecification("", location));
        List<BusStationResponse> responses = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        foreach (var t in responses)
        {
            t.AddressDb = await GetFullAddress(t.Address, t.WardId);
        }

        StationPagingResult result = AppUtils.ResultPaging<StationPagingResult, BusStationResponse>(
            pagingRequest.PageIndex, pagingRequest.PageSize,
            count, responses);
        return result;
    }

    public async Task<StationPagingResult> GetAllStationInBus(int busId, StationPaging pagingRequest)
    {
        BusStationSpecification specification = new BusStationSpecification(0, busId, paging: pagingRequest);
        List<BusStation> busStations = await _repository.ToList(specification);
        int count = await _repository.Count(new BusStationSpecification(0, busId));
        List<BusStationResponse> responses = await AppUtils.MapObject<BusStation, BusStationResponse>(busStations, _mapper);
        for (int i = 0; i < responses.Count; i++)
        {
            responses[i].AddressDb = await GetFullAddress(responses[i].Address, responses[i].WardId);
        }
        StationPagingResult result = AppUtils.ResultPaging<StationPagingResult, BusStationResponse>(
            pagingRequest.PageIndex, pagingRequest.PageSize,
            count, responses);
        return result;
    }

    #endregion

    #region -- Private Method --

    private async Task<string> GetFullAddress(string address, int wardId)
    {
        string addressDb = address + ", " + await AddressResponse.GetAddressDb(wardId, _wardService);
        return addressDb;
    }
    #endregion -- Private Method --

}