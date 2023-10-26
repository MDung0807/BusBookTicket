using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Repositories.BillItems;

public interface IBillItemRepos : IRepository<BillItem, int>
{
    Task<List<BillItem>> getAllItems(int billID);
}