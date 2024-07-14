namespace BusBookTicket.CustomerManage.Repository;

public interface ICustomerRepository
{
    /// <summary>
    /// Total Customer until date param
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task<int> TotalCustomer(DateTime dateTime);
    /// <summary>
    /// Rate Customer not active in total customer
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    Task<decimal> RateCustomer(DateTime dateTime);
}