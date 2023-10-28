using AutoMapper;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Responses.TicketItemRespositories;

namespace BusBookTicket.Ticket.Services.TicketItemServices;

public class TicketItemService : ITicketItemService
{

    #region -- Properties --

    private readonly ITicketItemRepos _repository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public TicketItemService(ITicketItemRepos repos, IMapper mapper)
    {
        this._repository = repos;
        this._mapper = mapper;
    }
    
    public async Task<TicketItemResponse> getByID(int id)
    {
        TicketItem item = await _repository.getByID(id);
        TicketItemResponse response = _mapper.Map<TicketItemResponse>(item);
        return response;
    }

    public Task<List<TicketItemResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> update(TicketItemForm entity, int id)
    {
        TicketItem item = _mapper.Map<TicketItem>(entity);
        await _repository.update(item);
        return true;
    }

    public Task<bool> delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(TicketItemForm entity)
    {
        TicketItem item = _mapper.Map<TicketItem>(entity);
        await _repository.create(item);
        return true;
    }

    public async Task<List<TicketItemResponse>> getAllInTicket(int ticketID)
    {
        List<TicketItem> items = await _repository.getAllItem(ticketID);
        List<TicketItemResponse> responses = await AppUtils.MappObject<TicketItem, TicketItemResponse>(items, _mapper);
        return responses;
    }
}