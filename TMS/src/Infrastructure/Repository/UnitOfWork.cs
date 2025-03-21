using Domain.IRepository;
using Infrastructure.Data;
using System.Collections;

namespace Infrastructure.Repository;

public class UnitOfWork(AppDBContext dbContext, ITicketRepository ticketRepository) : IUnitOfWork
{
    private readonly AppDBContext _dbContext = dbContext;
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private Hashtable repositories;

    public ITicketRepository TicketRepository => _ticketRepository;

    public void Dispose() => _dbContext.Dispose();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        if(repositories == null) repositories = new Hashtable();
        var type = typeof(TEntity).Name;

        if(!repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

            repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)repositories[type];
    }

    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public async Task<bool> SaveChangesReturnBoolAsync() => await _dbContext.SaveChangesAsync() > 0;
}
