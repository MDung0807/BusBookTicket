using AutoMapper;
using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;
using BusBookTicket.TicketManage.DTOs.Requests;
using BusBookTicket.TicketManage.DTOs.Responses;
using BusBookTicket.TicketManage.Repositories.Tickets;
using BusBookTicket.TicketManage.Services.TicketItems;
using BusBookTicket.TicketManage.Utilities;

namespace BusBookTicket.TicketManage.Services.Tickets;

public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITicketItemService _ticketItemService;

    public TicketService(IMapper mapper, ITicketRepository ticketRepository, ITicketItemService ticketItemService)
    {
        this._ticketItemService = ticketItemService;
        this._mapper = mapper;
        this._ticketRepository = ticketRepository;
    }
    public Task<TicketResponse> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> update(TicketRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(TicketRequest entity)
    {
        List<TicketItemRequest> itemsRequest = entity.itemsRequest;
        try
        {
            Ticket ticket = _mapper.Map<Ticket>(entity);
            int ticketId = await _ticketRepository.create(ticket);
            foreach (TicketItemRequest item in itemsRequest)
            {
                item.ticketID = ticketId;
                await _ticketItemService.create(item);
            }
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }

        return true;
    }
}