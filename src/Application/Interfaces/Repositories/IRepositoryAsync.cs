using Domain.Common.Contracts;

namespace Application.Interfaces.Repositories;

public interface IRepositoryAsync<T>  where T : BaseEntity
{
    IQueryable<T> Entities { get; }

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}