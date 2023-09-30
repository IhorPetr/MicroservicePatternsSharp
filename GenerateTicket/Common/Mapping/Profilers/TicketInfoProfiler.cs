using AutoMapper;
using Events.Ticket;
using GenerateTicket.Models;

namespace GenerateTicket.Common.Mapping.Profilers;

public class TicketInfoProfiler : Profile
{
    public TicketInfoProfiler()
    {
        CreateMap<IGenerateTicketEvent, TicketInfo>();
    }
}