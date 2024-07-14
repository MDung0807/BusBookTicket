using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Dapper;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.CompanyManage.Repository;

public class CompanyRepository: ICompanyRepository
{
    public async Task<int> TotalCompany(DateTime date)
    {
        string query = @"SELECT COUNT(*) AS TotalCompany  FROM Companies WHERE DateCreate <= @date";
        try
        {
            IDapperContext<int> dapperContext = new DapperContext<int>();
            var result = await dapperContext.ExecuteQueryAsync(query, new
            {
                date = date
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

    public async Task<decimal> Rate(DateTime date)
    {
        string query = @"SELECT COUNT(*) AS TotalCompany  FROM Companies WHERE DateCreate <= @date
                        UNION ALL
                        SELECT COUNT(*) AS TotalCompanyNotActive FROM Companies WHERE DateCreate <= GETDATE() AND Status <> @status";
        try
        {
            IDapperContext<int> dapperContext = new DapperContext<int>();
            var result = await dapperContext.ExecuteQueryAsync(query, new
            {
                date = date,
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