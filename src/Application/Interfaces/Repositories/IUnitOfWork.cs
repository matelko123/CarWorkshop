using Domain.Common.Contracts;

namespace Application.Interfaces.Repositories;

public interface IUnitOfWork<TId> : IDisposable
{
    IRepositoryAsync<T> Repository<T>() where T : BaseEntity;

    Task<int> Commit(CancellationToken cancellationToken = default);

    Task Rollback();
}

public interface IUnitOfWork : IUnitOfWork<Guid>
{
    
}