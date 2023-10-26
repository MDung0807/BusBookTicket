using AutoMapper;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Repositories.BillItems;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.BillManage.Services.BillItems;

public class BillItemService : IBillItemService
{
    private readonly IMapper _mapper;
    private readonly IBillItemRepos _billItemRepos;

    public BillItemService(IMapper mapper, IBillItemRepos billItemRepos)
    {
        this._mapper = mapper;
        this._billItemRepos = billItemRepos;
    }
    
    public Task<BillItemResponse> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BillItemResponse>> getAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> update(BillItemRequest entity, int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> create(BillItemRequest entity)
    {
        try
        {
            BillItem item = _mapper.Map<BillItem>(entity);
            await _billItemRepos.create(item);
            return true;
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_CREATE);
        }
    }

    public async Task<List<BillItemResponse>> GetItemInBill(int id)
    {
        List<BillItemResponse> responses = new List<BillItemResponse>();
        try
        {
            List<BillItem> ticketItems = await _billItemRepos.getAllItems(id);

            responses = await AppUtils.MappObject<BillItem, BillItemResponse>(ticketItems, _mapper);
            return responses;
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_GET);
        }
    }
}