using Application.Interfaces.Repositories;
using Domain.Common.Contracts;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryAsync<T> : IRepositoryAsync<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public RepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        T? exist = await GetByIdAsync(entity.Id, cancellationToken);
        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
    }
}