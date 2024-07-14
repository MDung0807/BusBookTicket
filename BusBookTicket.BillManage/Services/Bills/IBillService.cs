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

    /// <summary>
    /// Get Revenue Statistics in Company
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    Task<object> RevenueStatistics(int companyId, int year);
    
    /// <summary>
    /// Get Revenue Statistics in Company
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    Task<object> RevenueStatisticsByQuarter(int companyId, int year);

    /// <summary>
    /// Get Statistics Station by admin
    /// </summary>
    /// <param name="year"></param>
    /// <param name="take"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    Task<object> GetStatisticsStationByAdmin(int year, int take, bool desc = true);

    /// <summary>
    /// Get Statistics Station by company
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="year"></param>
    /// <param name="take"></param>
    /// <param name="desc"></param>
    /// <returns></returns>
    Task<object> GetStatisticsStationByCompany(int companyId, int year, int take, bool desc = true);

    /// <summary>
    /// Reserve for customer
    /// </summary>
    /// <param name="request">tickets</param>
    /// <param name="userId">userId for customer</param>
    /// <returns></returns>
    Task Reserve(BillRequest request, int userId);
    
    /// <summary>
    /// Check reserve cached. If cached return true, else return false
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <returns>bool</returns>
    Task<bool> CheckReserve(BillRequest request, int userId);

    /// <summary>
    /// Payment Paypal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="userId"></param>
    /// <returns>bool</returns>
    Task<bool> PaymentPaypal(BillRequest request, int userId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    Task<object> TotalBill(int companyId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    Task<object> Sales(int companyId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idMaster"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    Task<List<object>> Statistical(int idMaster, int month, int year);

    Task<List<object>> TopRouteInBill(int companyId, int top);
    
    /// <summary>
    /// Get Route By Admin
    /// </summary>
    /// <param name="top"></param>
    /// <returns></returns>
    Task<List<object>> TopRouteInBill(int top);
}