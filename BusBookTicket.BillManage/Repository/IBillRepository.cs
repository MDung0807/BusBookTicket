namespace BusBookTicket.BillManage.Repository;

public interface IBillRepository
{
    Task<List<object>> StatisticalInMonth(int idMaster, int month, int year);

    Task<decimal> Sales(int idMaster, int month, int year);
    Task<int> TotalBillInMonth(int idMaster, int month, int year);

    Task<List<object>> TopRouteInBill(int companyId, int top);
}