using Events.Ticket;

namespace SagaStateMachine;

public class GenerateTicketEvent : IGenerateTicketEvent
{
    private readonly TicketStateData _ticketStateData;
    public GenerateTicketEvent(TicketStateData ticketStateData)
    {
        _ticketStateData = ticketStateData;
    }
    public Guid TicketId => _ticketStateData.TicketId;

    public string Title => _ticketStateData.Title;

    public string Email => _ticketStateData.Email;

    public DateTime RequireDate => _ticketStateData.TicketCreatedDate;

    public int Age => _ticketStateData.Age;

    public string Location => _ticketStateData.Location;

    public string TicketNumber => _ticketStateData.TicketNumber;
}