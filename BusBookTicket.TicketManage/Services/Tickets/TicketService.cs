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
    public TicketResponse getByID(int id)
    {
        throw new NotImplementedException();
    }

    public List<TicketResponse> getAll()
    {
        throw new NotImplementedException();
    }

    public bool update(TicketRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public bool delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool create(TicketRequest entity)
    {
        List<TicketItemRequest> itemsRequest = entity.itemsRequest;
        List<TicketItem> items = AppUtils.MappObject<TicketItemRequest, TicketItem>(itemsRequest, _mapper);
        int ticketID;
        try
        {
            Ticket ticket = _mapper.Map<Ticket>(entity);
            ticketID = _ticketRepository.create(ticket);
            ticket = _ticketRepository.getByID(ticketID);
            foreach (TicketItem item in items)
            {
                item.ticket = ticket;
                // _ticketItemService.create()
            }
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }

        return true;
    }
}