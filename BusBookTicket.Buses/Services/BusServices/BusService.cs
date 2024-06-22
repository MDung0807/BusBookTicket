using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Paging.Bus;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Service;
using Microsoft.Extensions.DependencyInjection;

namespace BusBookTicket.Buses.Services.BusServices;

public class BusService : IBusService
{
    #region -- Properties --

    private readonly IMapper _mapper;
    private readonly IBusTypeService _busTypeService;
    private readonly ISeatTypeService _seatTypeService;
    private readonly ISeatService _seatService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Bus> _repository;
    private readonly IGenericRepository<StopStation> _stopStationRepository;
    private readonly IRoutesService _routesService;
    #endregion -- Properties --

    public BusService(
        IMapper mapper, 
        IBusTypeService busTypeService
        ,ISeatTypeService seatTypeService
        ,ISeatService seatService,
        IUnitOfWork unitOfWork,
        IRoutesService routesService)
    {
        this._mapper = mapper;
        this._seatTypeService = seatTypeService;
        this._busTypeService = busTypeService;
        this._seatService = seatService;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Bus>();
        _stopStationRepository = unitOfWork.GenericRepository<StopStation>();
        _routesService = routesService;
    }
    public async Task<BusResponse> GetById(int id)
    {
        BusSpecification busSpecification = new BusSpecification(id);
        Bus bus = await _repository.Get(busSpecification) ?? throw new Exception(BusConstants.Bus_NotAction);
        List<RoutesResponse> routesResponses = new List<RoutesResponse>();
        foreach (var stopStation in bus.StopStations)
        {
            routesResponses.Add(await _routesService.GetById(stopStation.Route.Id, companyId:bus.Company.Id));
        }
        BusResponse response = _mapper.Map<BusResponse>(bus);
        response.Routes = routesResponses;
        return response;
    }

    public async Task<List<BusResponse>> GetAll()
    {
        BusSpecification busSpecification = new BusSpecification(checkStatus:false);
        List<Bus> buses = await _repository.ToList(busSpecification);
        List<BusResponse> responses = await AppUtils.MapObject<Bus, BusResponse>(buses, _mapper);
        return responses;
    }

    public async Task<bool> Update(FormUpdateBus entity, int id, int userId)
    {
        Bus bus = _mapper.Map<Bus>(entity);
        bus.Id = id;
        bus.Status = (int)EnumsApp.Active;
        await _repository.Update(bus, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        BusSpecification busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true, dateTime:DateTime.Now);
        Bus bus = await _repository.Get(busSpecification, checkStatus: false);
        if (bus == null)
        {
            busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true);
            bus = await _repository.Get(busSpecification, checkStatus: false);
        }
        await _repository.ChangeStatus(bus, userId, (int)EnumsApp.Delete);
        return true;
    }

    public async Task<bool> Create(FormCreateBus entity, int userId)
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            //Get Data
            BusTypeResponse busType = await _busTypeService.GetById(entity.BusTypeId);
            SeatTypeResponse seatType = await _seatTypeService.GetById(entity.SeatTypeId);
            
            //Save Bus
            Bus bus = _mapper.Map<Bus>(entity);
            bus.Status = (int)EnumsApp.Active;
            bus = await _repository.Create(bus, userId);
            
            // foreach (int stationID in entity.ListBusStopId)
            // {
            //     BusStop busStop = new BusStop();
            //     busStop.BusStation ??= new BusStation();
            //     busStop.BusStation.Id = stationID;
            //     busStop.Bus ??= new Bus();
            //     busStop.Bus.Id= bus.Id;
            //     busStop.Bus.Status = (int)EnumsApp.Active;
            //
            //     await _busStopRepository.Create(busStop, userId);
            // }
            //
            foreach (int routeId in entity.ListRouteId)
            {
                StopStation stopStation = new StopStation();
                stopStation.Status = (int)EnumsApp.Active;
                stopStation.Bus = new Bus
                {
                    Id = bus.Id
                };
                stopStation.Route = new Routes
                {
                    Id = routeId
                };
            
                await _stopStationRepository.Create(stopStation, userId: userId);
            }
            
            int totalSeat = busType.TotalSeats == 0? 0: busType.TotalSeats;

            
            //Save Seat in bus
            for (int i = 0; i < totalSeat; i++)
            {
                SeatForm seatForm = new SeatForm();
                seatForm.SeatNumber = seatType.Type + "_" + i.ToString();
                seatForm.SeatTypeId = seatType.Id;
                seatForm.BusId = bus.Id;
                seatForm.Description = "";
                seatForm.Price = seatType.Price;
                seatForm.SeatId = 0;

                await _seatService.Create(seatForm, userId);
            }
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e);
            throw new Exception("ERRROR");
        }
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        BusSpecification busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true, dateTime:DateTime.Now);
        Bus bus = await _repository.Get(busSpecification, checkStatus: false);
        if (bus == null)
        {
            busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true);
            bus = await _repository.Get(busSpecification, checkStatus: false);
        }
        await _repository.ChangeStatus(bus, userId, (int)EnumsApp.Active);
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

    public async Task<bool> ChangeToDisable(int id, int userId)
    {
        BusSpecification busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true, dateTime:DateTime.Now);
        Bus bus = await _repository.Get(busSpecification, checkStatus: false);
        if (bus == null)
        {
            busSpecification = new BusSpecification(id, userId, false, getIsChangeStatus: true);
            bus = await _repository.Get(busSpecification, checkStatus: false);
        }
        await _repository.ChangeStatus(bus, userId, (int)EnumsApp.Disable);
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

    public Task<BusPagingResult> GetAllByAdmin(BusPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<BusPagingResult> GetAll(BusPaging pagingRequest)
    {
        throw new NotImplementedException();

    }

    public async Task<BusPagingResult> GetAll(BusPaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        BusSpecification busSpecification = new BusSpecification(idMaster:idMaster,checkStatus:checkStatus);
        List<Bus> buses = await _repository.ToList(busSpecification);
        int count = await _repository.Count(new BusSpecification(idMaster:idMaster, checkStatus:checkStatus));
        List<BusResponse> responses = new List<BusResponse>();
        foreach (var bus in buses)
        {
            List<RoutesResponse> routesResponses = new List<RoutesResponse>();
            foreach (var stopStation in bus.StopStations)
            {
                routesResponses.Add(await _routesService.GetById(stopStation.Route.Id));
            }

            BusResponse busResponse = _mapper.Map<BusResponse>(bus);
            busResponse.Routes = routesResponses;
            responses.Add(busResponse);
        }
        BusPagingResult result = AppUtils.ResultPaging<BusPagingResult, BusResponse>(pagingRequest.PageIndex,
            pagingRequest.PageSize, count, responses);
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BusResponse> AddBusStops(FormAddBusStop request, int userId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();
            
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e);
            throw new Exception("ERRROR");
        }

        return await GetById(request.Id);
    }

    public async Task<bool> RegisRoute(int id, int routeId, int userId)
    {
        StopStation stopStation = new StopStation
        {
            Status = (int)EnumsApp.Active,
            Route =
            {
                Id = routeId
            },
            Bus =
            {
                Id = id
            }
        };
        await _stopStationRepository.Create(stopStation, userId: userId);
        return true;
    }

    public async Task<BusPagingResult> GetInRoute(BusPaging paging, int companyId, int routeId)
    {
        BusSpecification specification = new BusSpecification(companyId: companyId, routeId: routeId, paging: paging);
        int count = await _repository.Count(new BusSpecification(companyId: companyId, routeId: routeId));
        List<Bus> buses = await _repository.ToList(specification);
        var result = AppUtils.ResultPaging<BusPagingResult, BusResponse>(
            paging.PageIndex, paging.PageSize,
            count: count,
            items: await AppUtils.MapObject<Bus, BusResponse>(buses, _mapper));
        return result;
    }
}