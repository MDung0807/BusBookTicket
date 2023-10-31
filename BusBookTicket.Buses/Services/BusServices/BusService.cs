using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.BusTypeRepositories;
using BusBookTicket.Buses.Services.SeatServices;
using BusBookTicket.Buses.Services.SeatTypServices;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public class BusService : IBusService
{
    #region -- Properties --

    private readonly IBusRepos _busRepos;
    private readonly IMapper _mapper;
    private readonly IBusTypeService _busTypeService;
    private readonly ISeatTypeService _seatTypeService;
    private readonly ISeatService _seatService;
    private readonly IUnitOfWork _unitOfWork;
    #endregion -- Properties --

    public BusService(
        IBusRepos busRepos, 
        IMapper mapper, 
        IBusTypeService busTypeService
        ,ISeatTypeService seatTypeService
        ,ISeatService seatService,
        IUnitOfWork unitOfWork)
    {
        this._busRepos = busRepos;
        this._mapper = mapper;
        this._seatTypeService = seatTypeService;
        this._busTypeService = busTypeService;
        this._seatService = seatService;
        this._unitOfWork = unitOfWork;
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
        await _unitOfWork.BeginTransaction();
        try
        {
            //Get Data
            BusTypeResponse busType = await _busTypeService.getByID(entity.busTypeID);
            SeatTypeResponse seatType = await _seatTypeService.getByID(entity.seatTypeID);
            
            //Save Bus
            Bus bus = _mapper.Map<Bus>(entity);
            bus.status = (int)EnumsApp.Active;
            int busID = await _busRepos.create(bus);
            
            foreach (int stationID in entity.listBusStopID)
            {
                BusStop busStop = new BusStop();
                busStop.BusStation ??= new BusStation();
                busStop.BusStation.busStationID = stationID;
                busStop.bus ??= new Bus();
                busStop.bus.busID= busID;

                await _busRepos.createStopStation(busStop);
            }
            
            int totalSeat = busType.totalSeats == 0? 1: busType.totalSeats;

            
            //Save Seat in bus
            for (int i = 0; i < totalSeat; i++)
            {
                SeatForm seatForm = new SeatForm();
                seatForm.seatNumber = seatType.type + "_" + i.ToString();
                seatForm.seatTypeID = seatType.typeID;
                seatForm.busID = busID;
                seatForm.description = "";
                seatForm.price = seatType.price;
                seatForm.seatID = 0;

                await _seatService.create(seatForm);
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
}