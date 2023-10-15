using AutoMapper;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;
using BusBookTicket.TicketManage.DTOs.Requests;
using BusBookTicket.TicketManage.DTOs.Responses;
using BusBookTicket.TicketManage.Repositories.TicketItems;
using BusBookTicket.TicketManage.Utilities;

namespace BusBookTicket.TicketManage.Services.TicketItems;

public class TicketItemService : ITicketItemService
{
    private readonly IMapper _mapper;
    private readonly ITicketItemRepos _ticketItemRepos;

    public TicketItemService(IMapper mapper, ITicketItemRepos ticketItemRepos)
    {
        this._mapper = mapper;
        this._ticketItemRepos = ticketItemRepos;
    }
    
    public TicketItemResponse getByID(int id)
    {
        throw new NotImplementedException();
    }

    public List<TicketItemResponse> getAll()
    {
        throw new NotImplementedException();
    }

    public bool update(TicketItemRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public bool delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool create(TicketItemRequest entity)
    {
        try
        {
            TicketItem item = _mapper.Map<TicketItem>(entity);
            _ticketItemRepos.create(item);
            return true;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_CREATE);
        }
    }

    public List<TicketItemResponse> GetItemInTicket(int id)
    {
        List<TicketItemResponse> responses = new List<TicketItemResponse>();
        try
        {
            List<TicketItem> ticketItems = _ticketItemRepos.getAllItems(id);

            responses = AppUtils.MappObject<TicketItem, TicketItemResponse>(ticketItems, _mapper);
            return responses;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }
}