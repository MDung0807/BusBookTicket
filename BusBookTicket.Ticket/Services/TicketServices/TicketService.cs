using AutoMapper;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Specification;

namespace BusBookTicket.Ticket.Services.TicketServices;

public class TicketService : ITicketService
{
    #region -- Properties --

    private readonly ITicketItemService _itemService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Core.Models.Entity.Ticket> _repository;
    private readonly IGenericRepository<Bus> _busRepository;
    private readonly IGenericRepository<Ticket_BusStop> _ticketBusStop;
    #endregion -- Properties --

    public TicketService(
        ITicketItemService itemService, 
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        this._itemService = itemService;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
        _busRepository = unitOfWork.GenericRepository<Bus>();
        _ticketBusStop = unitOfWork.GenericRepository<Ticket_BusStop>();
    }
    
    public async Task<TicketResponse> GetById(int id)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(id);
        Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
        List<TicketItemResponse> itemResponses = await _itemService.getAllInTicket(id);
        TicketResponse response = _mapper.Map<Core.Models.Entity.Ticket, TicketResponse>(ticket);
        response.ItemResponses = itemResponses;
        return response;
    }

    public Task<List<TicketResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(TicketFormUpdate entity, int id, int userId)
    {
        Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
        await _repository.Update(ticket, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(id);
        Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
        ticket.Status = (int)EnumsApp.Delete;
        await _repository.Delete(ticket, userId);
        return true;
    }

    public async Task<bool> Create(TicketFormCreate entity, int userId)
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            //Create Ticket
            Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
            ticket.Status = (int)EnumsApp.Active;
            ticket = await _repository.Create(ticket, userId);

            Ticket_BusStop ticketBusStop = new Ticket_BusStop();
            foreach (var ticketStation in entity.TicketStations)
            {
                ticketBusStop = _mapper.Map<Ticket_BusStop>(ticketStation);
                ticketBusStop.Ticket = new Core.Models.Entity.Ticket
                {
                    Id = ticket.Id
                };
                await _ticketBusStop.Create(ticketBusStop, userId);
            }
            
            // Create TicketItem
            TicketSpecification ticketSpecification = new TicketSpecification(ticket.Id);
            BusSpecification busSpecification = new BusSpecification(entity.BusId);
            Bus bus = await _busRepository.Get(busSpecification);
            ticket = await _repository.Get(ticketSpecification);
            ticket.Bus = bus;
            List<Seat> seats = ticket.Bus.Seats.ToList();
            foreach (Seat seat in seats)
            {
                await createItem(seat, ticket.Id, userId);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e.ToString());
            throw new Exception("ERROR");
        }
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

    public Task<List<TicketResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// FindTicket
    /// </summary>
    /// <param name="searchForm"></param>
    /// <returns></returns>
    public async Task<List<TicketResponse>> GetAllTicket(SearchForm searchForm)
    {
        //searchForm.dateTime = Convert.ToDateTime(searchForm.dateTime);

        
        TicketSpecification ticketSpecification =
            new TicketSpecification(dateTime: searchForm.dateTime, 
                stationStart: searchForm.stationStart,
                stationEnd: searchForm.stationEnd);
        
        string sqlQuery = sqlFindTicket(searchForm.stationStart, searchForm.stationEnd, searchForm.dateTime);

        List<Core.Models.Entity.Ticket> ticket = await _repository.ToListWithSqlQuery(sqlQuery);
        // Find
        List<TicketResponse> responses = new List<TicketResponse>();

        foreach (var item in ticket)
        {
            responses.Add(await GetById(item.Id));
        }

        return responses;
    }

    #region -- Private Method --

    private async Task<bool> createItem(Seat seat, int ticketID, int userId)
    {
        TicketItemForm form = new TicketItemForm();
        form.ticketID = ticketID;
        form.status = seat.Status;
        form.ticketItemID = 0;
        form.seatNumber = seat.SeatNumber;
        form.price = seat.Price;

        return await _itemService.Create(form, userId);
    }
    #endregion -- Private Method --

    #region -- Query --
    private string sqlFindTicket(string stationStart, string stationEnd, DateTime dateTime)
    {
        string query = @"
        SELECT
            T.Id,
            T.Date,
            T.BusID,
            T.DateCreate,
            T.DateUpdate,
            T.UpdateBy,
            T.CreateBy,
            T.Status,
            Bus.BusNumber,
            Companies.Name
        FROM
            Ticket_BusStop t1
            INNER JOIN Tickets T ON T.Id = t1.TicketId
            LEFT JOIN TicketItems TItem ON TItem.TicketID = T.Id
            LEFT JOIN Buses Bus ON Bus.Id = T.BusID
            LEFT JOIN Companies ON Companies.Id = Bus.CompanyID
        WHERE
            t1.BusStopId IN (
                SELECT BS.Id
                FROM BusStations B
                    LEFT JOIN Wards W ON B.WardId = W.Id
                    LEFT JOIN Districts D ON W.DistrictId = D.Id
                    LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                    INNER JOIN BusStops BS ON BS.BusStationID = B.Id
                WHERE
                    B.Name LIKE N'%@StationStart%'
                    OR W.FullName LIKE N'%@StationStart%'
                    OR D.FullName LIKE N'%@StationStart%'
                    OR P.FullName LIKE N'%@StationStart%'
            )
            AND t1.DepartureTime > '@DateTime'
            AND EXISTS (
                SELECT 1
                FROM Ticket_BusStop t2
                WHERE
                    t2.TicketId = t1.TicketId
                    AND t2.BusStopId IN (
                        SELECT BS.Id
                        FROM BusStations B
                            LEFT JOIN Wards W ON B.WardId = W.Id
                            LEFT JOIN Districts D ON W.DistrictId = D.Id
                            LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                            INNER JOIN BusStops BS ON BS.BusStationID = B.Id
                        WHERE
                            B.Name LIKE N'%@StationEnd%'
                            OR W.FullName LIKE N'%@StationEnd%'
                            OR D.FullName LIKE N'%@StationEnd%'
                            OR P.FullName LIKE N'%@StationEnd%'
                    )
                    AND t2.IndexStation > t1.IndexStation
            )
        GROUP BY
            T.Id,
            T.Date,
            T.BusID,
            T.DateCreate,
            T.DateUpdate,
            T.UpdateBy,
            T.CreateBy,
            T.Status,
            Bus.BusNumber,
            Companies.Name;
    ";
        // Replace placeholders with actual parameter names
        query = query.Replace("@StationStart", stationStart)
                     .Replace("@StationEnd", stationEnd)
                     .Replace("@DateTime", dateTime.ToString("yyyy-MM-dd HH:mm:ss"));  // Ensure the correct date format

        return query;
    }

    #endregion -- Query --
}