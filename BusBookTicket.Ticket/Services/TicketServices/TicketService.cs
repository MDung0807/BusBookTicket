using AutoMapper;
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
            
            //Create TicketItem
            TicketSpecification ticketSpecification = new TicketSpecification(ticket.Id);
            ticket = await _repository.Get(ticketSpecification);
            
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

    public async Task<List<TicketResponse>> GetAllTicket(SearchForm searchForm)
    {
        searchForm.dateTime = searchForm.dateTime.Date;
        searchForm.dateTime = Convert.ToDateTime("30/10/2023");
        
        TicketSpecification ticketSpecification =
            new TicketSpecification(dateTime: searchForm.dateTime, 
                stationStart: searchForm.stationStart,
                stationEnd: searchForm.stationEnd);
        
        // Find
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        List<TicketResponse> responses = new List<TicketResponse>();

        // Map
        foreach (Core.Models.Entity.Ticket ticket in tickets)
        {
            responses.Add( await GetById(ticket.Id));
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
}