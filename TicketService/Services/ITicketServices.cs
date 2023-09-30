using TicketService.Models;

namespace TicketService.Services;

public interface ITicketServices
{
    Task<Ticket> AddTicket(Ticket ticket);
    bool DeleteTicket(string TicketId); 
}