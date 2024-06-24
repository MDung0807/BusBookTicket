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
        this._repository = _unitOfWork.GenericRepository<TicketItem>();
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

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        TicketItemSpecification specification = new TicketItemSpecification(id, checkStatus:false, isGetChangeStatus:true, action:"BillItem");
        TicketItem item = await _repository.Get(specification, checkStatus: false);
        await _repository.ChangeStatus(item, userId, (int)EnumsApp.Active);
        return true;
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

    public Task<object> GetAllByAdmin(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketItemResponse>> GetAllInTicket(int ticketId)
    {
        TicketItemSpecification ticketItemSpecification = new TicketItemSpecification(0, ticketId, checkStatus:false);
        List<TicketItem> items = await _repository.ToList(ticketItemSpecification);
        List<TicketItemResponse> responses = await AppUtils.MapObject<TicketItem, TicketItemResponse>(items, _mapper);
        return responses;
    }

    public async Task<bool> ChangeStatusToWaitingPayment(int id, int userId)
    {
        TicketItemSpecification ticketItemSpecification = new TicketItemSpecification(id);
        TicketItem item = await _repository.Get(ticketItemSpecification, checkStatus: false);
        List<Dictionary<string, int>> listCheckObject = 
            new List<Dictionary<string, int>> { new Dictionary<string, int> { { "Ticket", 0 } } };
        return await _repository.ChangeStatus(item, userId, (int)EnumsApp.AwaitingPayment, listCheckObject);
    }

    public async Task<bool> ChangeStatusToPaymentComplete(int id, int userId)
    {
        TicketItemSpecification ticketItemSpecification = new TicketItemSpecification(id);
        TicketItem item = await _repository.Get(ticketItemSpecification, checkStatus: false);
        return await _repository.ChangeStatus(item, userId, (int)EnumsApp.PaymentComplete);
    }
}