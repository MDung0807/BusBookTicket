using AutoMapper;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Paging;
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
        List<TicketItemResponse> itemResponses = await _itemService.GetAllInTicket(id);
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

    public Task<TicketPagingResult> GetAllByAdmin(TicketPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<TicketPagingResult> GetAll(TicketPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<TicketPagingResult> GetAll(TicketPaging pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
    }
    
    public async Task<TicketPagingResult> GetAllTicket(SearchForm searchForm, TicketPaging paging)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(searchForm.StationStart, searchForm.StationEnd, searchForm.DateTime,paging);
        List<Core.Models.Entity.Ticket> ticket = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(searchForm.StationStart, searchForm.StationEnd,
            searchForm.DateTime));
        // Find
        List<TicketResponse> responses = new List<TicketResponse>();

        foreach (var item in ticket)
        {
            responses.Add(await GetById(item.Id));
        }

        TicketPagingResult result = AppUtils.ResultPaging<TicketPagingResult, TicketResponse>(
            paging.PageIndex, paging.PageSize, count: count, responses);
        return result;
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
}