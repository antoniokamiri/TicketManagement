using Domain.DTO.Requests;
using Domain.Entities;

namespace Domain.IRepository;

public interface IUnitOfWork : IDisposable
{
    ITicketRepository TicketRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
    Task<bool> SaveChangesReturnBoolAsync();
}

public interface ITicketRepository : IGenericRepository<Ticket>
{
    List<Ticket> GetTickets(GetTicketsRequest request);
}