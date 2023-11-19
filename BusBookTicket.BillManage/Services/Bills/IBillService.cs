using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.BillManage.Services.Bills;

public interface IBillService: IService<BillRequest, BillRequest, int, BillResponse>
{
    Task<bool> ChangeStatusToWaitingPayment(int id, int userId);
    Task<bool> ChangeStatusToPaymentComplete(int id, int userId);

    Task<List<BillResponse>> GetAllBillInUser(int userId);
}