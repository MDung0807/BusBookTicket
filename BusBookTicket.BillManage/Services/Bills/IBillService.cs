using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.BillManage.Services.Bills;

public interface IBillService: IService<BillRequest, BillRequest, int, BillResponse, BillPaging, BillPagingResult>
{
    Task<bool> ChangeStatusToWaitingPayment(int id, int userId);
    Task<bool> ChangeStatusToPaymentComplete(int id, int userId);

    /// <summary>
    /// Get all bill in Customer
    /// </summary>
    /// <param name="paging">paging</param>
    /// <param name="userId">id customer</param>
    /// <returns></returns>
    Task<BillPagingResult> GetAllBillInUser(BillPaging paging, int userId);
}