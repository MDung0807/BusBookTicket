namespace BusBookTicket.BillManage.Repository;

public interface IBillRepository
{
    Task<List<object>> Statistical(int idMaster, int month, int year);
}