using AutoMapper;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Repositories.Bills;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Services.Bills;

public class BillService : IBillService
{
    private readonly IMapper _mapper;
    private readonly IBillRepository _billRepository;
    private readonly IBillItemService _billItemService;

    public BillService(IMapper mapper, IBillRepository billRepository, IBillItemService billItemService)
    {
        this._billItemService = billItemService;
        this._mapper = mapper;
        this._billRepository = billRepository;
    }
    public Task<BillResponse> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BillResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> update(BillRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(BillRequest entity)
    {
        List<BillItemRequest> itemsRequest = entity.itemsRequest;
        try
        {
            Bill bill = _mapper.Map<Bill>(entity);
            int billID = await _billRepository.create(bill);
            foreach (BillItemRequest item in itemsRequest)
            {
                item.billID = billID;
                await _billItemService.create(item);
            }
        }
        catch
        {
            throw new Exception(BillConstants.ERROR);
        }

        return true;
    }
}