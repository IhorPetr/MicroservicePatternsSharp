using AutoMapper;
using TicketService.Dtos;
using TicketService.Models;

namespace TicketService.Common.Mapping.Profiles;

public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<AddTicketDto, Ticket>();
        CreateMap<Ticket, ResponseTicketDto>();
    }
}