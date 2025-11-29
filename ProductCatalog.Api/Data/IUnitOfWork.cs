namespace ProductCatalog.Api.Data;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync(CancellationToken ct = default);
    Task CommitAsync(CancellationToken ct = default);
}
