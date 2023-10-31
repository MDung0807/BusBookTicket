using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BusBookTicket.Core.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDBContext context)
    {
        _context = context;
    }
    public async Task SaveChangesAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task BeginTransaction()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _context.Dispose();
    }
}


    public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task BeginTransaction();
    Task RollbackTransactionAsync();
}