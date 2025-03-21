using Domain.DTO.Requests;
using Domain.DTO.Response;

namespace Domain.Interfaces;

public interface ITicketService
{
    List<GetTicketsResponse> GetTickets(GetTicketsRequest request);
    GetTicketsResponse FindTicket(int ticketId);
}