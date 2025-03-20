using Domain.IRepository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Infrastructure.Repository;
public class GenericRepository<T>(IdentityDbContext dbContext) : IGenericRepository<T> where T : class
{
    internal readonly IdentityDbContext _dbContext = dbContext;
    public void Add(T entity) => _dbContext.Set<T>().Add(entity);

    public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

    public List<T> GetAll() => [.. _dbContext.Set<T>()];

    public T GetById(int id) => _dbContext.Set<T>().Find(id);

    public void SaveChanges() => _dbContext.SaveChanges();

    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}

public class UnitOfWork(IdentityDbContext dbContext) : IUnitOfWork
{
    private readonly IdentityDbContext _dbContext = dbContext;
    private Hashtable repositories;

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
