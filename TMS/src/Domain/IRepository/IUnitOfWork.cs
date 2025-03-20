namespace Domain.IRepository;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
    Task<bool> SaveChangesReturnBoolAsync();
}