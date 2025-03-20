namespace Domain.IRepository;
public interface IGenericRepository<T> where T : class
{
    T GetById(int id);
    List<T> GetAll();
    void Delete(T entity);
    void Update(T entity);
    void Add(T entity);
    void SaveChanges();
}
