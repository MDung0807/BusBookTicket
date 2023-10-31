using AutoMapper;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Responses.TicketRepositories;
using BusBookTicket.Ticket.Services.TicketItemServices;

namespace BusBookTicket.Ticket.Services.TicketServices;

public class TicketService : ITicketService
{
    #region -- Properties --

    private readonly ITicketRepository _repository;
    private readonly ITicketItemService _itemService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    #endregion -- Properties --

    public TicketService(ITicketItemService itemService, ITicketRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._repository = repository;
        this._itemService = itemService;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
    }
    
    public async Task<TicketResponse> getByID(int id)
    {
        Core.Models.Entity.Ticket ticket = await _repository.getByID(id);
        List<TicketItemResponse> itemResponses = await _itemService.getAllInTicket(id);
        TicketResponse response = _mapper.Map<Core.Models.Entity.Ticket, TicketResponse>(ticket);
        response.ItemResponses = itemResponses;
        return response;
    }

    public Task<List<TicketResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> update(TicketFormUpdate entity, int id)
    {
        Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
        await _repository.update(ticket);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Core.Models.Entity.Ticket ticket = await _repository.getByID(id);
        ticket.status = (int)EnumsApp.Delete;
        await _repository.delete(ticket);
        return true;
    }

    public async Task<bool> create(TicketFormCreate entity)
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
            int ticketID = await _repository.create(ticket);
            ticket = await _repository.getByID(ticketID);
            List<Seat> seats = ticket.bus.seats.ToList();
            foreach (Seat seat in seats)
            {
                TicketItemForm form = new TicketItemForm();
                form.ticketID = ticketID;
                form.status = seat.status;
                form.ticketItemID = 0;
                form.seatNumber = seat.seatNumber;
                form.price = seat.price;

                await _itemService.create(form);
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

    public async Task<List<TicketResponse>> getAllTicket(DateTime dateTime, string stationStart, string stationEnd)
    {
        List<Core.Models.Entity.Ticket> tickets = await _repository.getAllTicket(dateTime, stationStart, stationEnd);
        List<TicketResponse> responses = new List<TicketResponse>();

        foreach (Core.Models.Entity.Ticket ticket in tickets)
        {
            responses.Add( await getByID(ticket.ticketID));
        }

        return responses;
    }
}