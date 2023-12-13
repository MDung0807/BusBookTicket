using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.BillManage.Services.Bills;

public interface IBillService: IService<BillRequest, BillRequest, int, BillResponse, BillPaging, BillPagingResult>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ChangeStatusToWaitingPayment(int id, int userId);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ChangeStatusToPaymentComplete(int id, int userId);

    /// <summary>
    /// Get all bill in Customer
    /// </summary>
    /// <param name="paging">paging</param>
    /// <param name="userId">id customer</param>
    /// <returns></returns>
    Task<BillPagingResult> GetAllBillInUser(BillPaging paging, int userId);

    /// <summary>
    /// Change status in bill is Complete (status = 1)
    /// </summary>
    /// <param name="billId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> ChangeCompleteStatus(int billId, int userId);

    /// <summary>
    /// Response bill for user, bus
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="busId"></param>
    /// <returns></returns>
    Task<BillResponse> GetBillByUserAndBus(int userId, int busId);
    
    /// <summary>
    /// Get all bill for user have status is waiting
    /// </summary>
    /// <param name="paging"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BillPagingResult> GetAllInWaitingStatus(BillPaging paging, int userId);

    /// <summary>
    /// Get all bill for user have status is canceled
    /// </summary>
    /// <param name="paging"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BillPagingResult> GetAllInDeleteStatus(BillPaging paging, int userId);
    
    /// <summary>
    /// Get all bill for user have status is complete
    /// </summary>
    /// <param name="paging"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BillPagingResult> GetAllInCompleteStatus(BillPaging paging, int userId);


}