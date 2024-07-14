namespace BusBookTicket.CompanyManage.Repository;

public interface ICompanyRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    Task<int> TotalCompany(DateTime date);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    Task<decimal> Rate(DateTime date);

}