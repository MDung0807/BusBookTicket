using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Specification;

namespace BusBookTicket.Ticket.Services.TicketItemServices;

public class TicketItemService : ITicketItemService
{

    #region -- Properties --

    private readonly IGenericRepository<TicketItem> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public TicketItemService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<TicketItem>();
    }
    
    public async Task<TicketItemResponse> GetById(int id)
    {
        TicketItemSpecification ticketItemSpecification = new TicketItemSpecification(id);
        TicketItem item = await _repository.Get(ticketItemSpecification);
        TicketItemResponse response = _mapper.Map<TicketItemResponse>(item);
        return response;
    }

    public Task<List<TicketItemResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(TicketItemForm entity, int id, int userId)
    {
        TicketItem item = _mapper.Map<TicketItem>(entity);
        await _repository.Update(item, userId);
        return true;
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(TicketItemForm entity, int userId)
    {
        TicketItem item = _mapper.Map<TicketItem>(entity);
        await _repository.Create(item, userId);
        return true;
    }

    public async Task<List<TicketItemResponse>> getAllInTicket(int ticketID)
    {
        TicketItemSpecification ticketItemSpecification = new TicketItemSpecification(0, ticketID);
        List<TicketItem> items = await _repository.ToList(ticketItemSpecification);
        List<TicketItemResponse> responses = await AppUtils.MappObject<TicketItem, TicketItemResponse>(items, _mapper);
        return responses;
    }
}