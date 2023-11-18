using System.Linq.Expressions;
using BusBookTicket.Core.Application.Specification.Interfaces;

namespace BusBookTicket.Core.Application.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    /// <summary>
    /// Create Criteria in query
    /// </summary>
    /// <param name="criteria">Criteria</param>
    /// <param name="checkStatus">Default is True, If True then query add condition is active, else query not check condition status</param>
    protected BaseSpecification(Expression<Func<T, bool>> criteria, bool checkStatus = true)
    {
        Criteria = criteria;
        CheckStatus = checkStatus;
    }
    protected BaseSpecification()
    {

    }
    public Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public bool CheckStatus { get; }
    public List<string> IncludeStrings { get; } = new List<string>();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    public Expression<Func<T, object>> GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; } = false;

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }

    protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
    
}