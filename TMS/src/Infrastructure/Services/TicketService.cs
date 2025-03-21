using Domain.DTO.Requests;
using Domain.DTO.Response;
using Domain.Entities;
using Domain.Interfaces;
using Domain.IRepository;

namespace Infrastructure.Services;

public class TicketService(IUnitOfWork unitOfWork) : ITicketService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public GetTicketsResponse FindTicket(int ticketId)
    {
        var result = _unitOfWork.Repository<Ticket>().GetById(ticketId);
        if (result == null) return null;

        return new GetTicketsResponse
        {
            Id = result.Id,
            Summary = result.Summary,
            Description = result.Description,
            Status = result.Status,
            Priority_Id = result.Priority_Id,
            Product_Id = result.Product_Id,
            Category_Id = result.Category_Id,
            Expected_Date = result.Expected_Date,
            Raised_Date = result.Raised_Date,
            Raised_By_Name = result.Raised_By?.DisplayName,
            Assigned_To_Id = result.Assigned_To?.Id,
            Assigned_To_Name = result.Assigned_To?.DisplayName,
            Priority_Name = result.Priority?.Name,
            Product_Name = result.Product?.Name,
            Category_Name = result.Category?.Name
        };
    }

    public List<GetTicketsResponse> GetTickets(GetTicketsRequest request)
    {
       var result = _unitOfWork.TicketRepository.GetTickets(request);

        return result.Select(x => new GetTicketsResponse
        {
            Id = x.Id,
            Summary = x.Summary,
            Description = x.Description,
            Status = x.Status,
            Priority_Id = x.Priority_Id,
            Product_Id = x.Product_Id,
            Category_Id = x.Category_Id,
            Expected_Date = x.Expected_Date,
            Raised_By_Id = x.Raised_By?.DisplayName,
            Raised_Date = x.Raised_Date,
            Assigned_To_Id = x.Assigned_To?.Id,
            Assigned_To_Name = x.Assigned_To?.DisplayName,
            Priority_Name = x.Priority?.Name,
            Product_Name = x.Product?.Name,
            Category_Name = x.Category?.Name
        }).ToList();
    }
}