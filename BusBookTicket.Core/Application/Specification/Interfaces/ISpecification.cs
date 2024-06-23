using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace BusBookTicket.Core.Application.Specification.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    bool CheckStatus { get; }
    List<string> IncludeStrings { get; }
    public List<Func<IQueryable<T>, IQueryable<T>>> IncludeConditions { get; }

    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }
    string SqlQuery { get; }

    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    void AddSqlQuery(string sqlQuery);
    void AddCriteria(Expression<Func<T, bool>> additionalCriteria);
    IEnumerable<SqlParameter> GetParameters();
}