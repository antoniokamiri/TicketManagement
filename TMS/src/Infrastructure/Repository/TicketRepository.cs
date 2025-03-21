using Domain.DTO.Requests;
using Domain.Entities;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class TicketRepository(AppDBContext dbContext) : GenericRepository<Ticket>(dbContext), ITicketRepository
{
    public List<Ticket> GetTickets(GetTicketsRequest request)
    {
        IQueryable<Ticket> query = _dbContext.Set<Ticket>()
            .Include(x => x.Category)
            .Include(x => x.Priority)
            .Include(x => x.Product)
            .Include(x => x.Assigned_To)
            .Include(x => x.Raised_By);

        if(request is null) return [.. query];

        if(!string.IsNullOrEmpty(request.Summary))
        {
            query = query.Where(x => (EF.Functions.Like(x.Summary, $"%{request.Summary}%"))); // SQL Like query
        }

        if (request.Raised_By_Id != null && request.Raised_By_Id.Count() > 0)
        {
            query = query.Where(x => request.Raised_By_Id.Contains(x.Raised_By_Id));
        }

        if (request.Status != null && request.Status.Count() > 0)
        {
            query = query.Where(x => request.Status.Contains(x.Status));
        }

        if(request.Product_Id != null && request.Product_Id.Count() > 0)
        {
            query = query.Where(x => request.Product_Id.Contains(x.Product_Id));
        }

        if (request.Category_Id != null && request.Category_Id.Count() > 0)
        {
            query = query.Where(x => request.Category_Id.Contains(x.Category_Id));
        }

        if (request.Priority_Id != null && request.Priority_Id.Count() > 0)
        {
            query = query.Where(x => request.Priority_Id.Contains(x.Priority_Id));
        }

        return [.. query.OrderByDescending(x => x.Raised_Date)];
    }
}