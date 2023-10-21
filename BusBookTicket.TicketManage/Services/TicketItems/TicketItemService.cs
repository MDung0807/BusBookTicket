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
    
    public Task<TicketItemResponse> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketItemResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> update(TicketItemRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(TicketItemRequest entity)
    {
        try
        {
            TicketItem item = _mapper.Map<TicketItem>(entity);
            await _ticketItemRepos.create(item);
            return true;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_CREATE);
        }
    }

    public async Task<List<TicketItemResponse>> GetItemInTicket(int id)
    {
        List<TicketItemResponse> responses = new List<TicketItemResponse>();
        try
        {
            List<TicketItem> ticketItems = await _ticketItemRepos.getAllItems(id);

            responses = await AppUtils.MappObject<TicketItem, TicketItemResponse>(ticketItems, _mapper);
            return responses;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }
}