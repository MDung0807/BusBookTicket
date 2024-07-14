using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Dapper;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.CustomerManage.Repository;

public class CustomerRepository : ICustomerRepository
{
    public async Task<int> TotalCustomer(DateTime dateTime)
    {
        string query = @"SELECT COUNT(*) AS TotalCustomer  FROM Customers WHERE DateCreate <= @date";
        try
        {
            IDapperContext<int> dapperContext = new DapperContext<int>();
            var result = await dapperContext.ExecuteQueryAsync(query, new
            {
                date = dateTime
            });

            if (result != null && result.Count > 0)
            {
                return result.FirstOrDefault();
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new ExceptionDetail(ex.Message);
        }
    }

    public async Task<decimal> RateCustomer(DateTime dateTime)
    {
        string query = @"SELECT COUNT(*) AS TotalCustomer  FROM Customers WHERE DateCreate <= @date
                        UNION ALL
                        SELECT COUNT(*) AS TotalCustomerNotActive FROM Customers WHERE DateCreate <= GETDATE() AND Status <> @status";
        try
        {
            IDapperContext<int> dapperContext = new DapperContext<int>();
            var result = await dapperContext.ExecuteQueryAsync(query, new
            {
                date = dateTime,
                status = (int)EnumsApp.Active
            });

            if (result != null && result.Count > 0)
            {
                int totalCustomerNotActive = result[^1];
                int totalCustomer = result.FirstOrDefault();
                return (decimal)totalCustomerNotActive *100 /totalCustomer;
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new ExceptionDetail(ex.Message);
        }
    }
}