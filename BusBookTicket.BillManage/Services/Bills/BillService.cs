using AutoMapper;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Services.Bills;

public class BillService : IBillService
{
    private readonly IMapper _mapper;
    private readonly IBillItemService _billItemService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Bill> _repository;

    public BillService(
        IMapper mapper,
        IBillItemService billItemService,
        IUnitOfWork unitOfWork
        )
    {
        this._billItemService = billItemService;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Bill>();
        this._mapper = mapper;
    }
    public Task<BillResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BillResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(BillRequest entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(BillRequest entity, int userId)
    {
        List<BillItemRequest> itemsRequest = entity.itemsRequest;
        try
        {
            Bill bill = _mapper.Map<Bill>(entity);
            bill = await _repository.Create(bill, userId);
            foreach (BillItemRequest item in itemsRequest)
            {
                item.billID = bill.Id;
                await _billItemService.Create(item, userId);
            }
        }
        catch
        {
            throw new Exception(BillConstants.ERROR);
        }

        return true;
    }
}