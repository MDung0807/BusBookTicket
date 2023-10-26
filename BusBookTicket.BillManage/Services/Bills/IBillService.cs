using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.Core.Common;

namespace BusBookTicket.BillManage.Services.Bills;

public interface IBillService: IService<BillRequest, BillRequest, int, BillResponse>
{
    
}