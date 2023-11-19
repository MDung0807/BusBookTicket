using AutoMapper;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Specification;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.BillManage.Services.BillItems;

public class BillItemService : IBillItemService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<BillItem> _repository;

    public BillItemService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<BillItem>();
        this._mapper = mapper;
    }
    
    public Task<BillItemResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BillItemResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(BillItemRequest entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(BillItemRequest entity, int userId)
    {
        try
        {
            await CreateBillItem(entity, userId);
            return true;
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_CREATE);
        }
    }
    
    public async Task<BillItem> CreateBillItem(BillItemRequest entity, int userId)
    {
        try
        {
            BillItem item = _mapper.Map<BillItem>(entity);
            item.Status = (int)EnumsApp.AwaitingPayment;
            return await _repository.Create(item, userId);
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_CREATE);
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

    public Task<List<BillItemResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }

    public async Task<List<BillItemResponse>> GetItemInBill(int id)
    {
        List<BillItemResponse> responses = new List<BillItemResponse>();
        try
        {
            BillItemSpecification billItemSpecification = new BillItemSpecification(id);
            List<BillItem> ticketItems = await _repository.ToList(billItemSpecification);

            responses = await AppUtils.MappObject<BillItem, BillItemResponse>(ticketItems, _mapper);
            return responses;
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_GET);
        }
    }
}