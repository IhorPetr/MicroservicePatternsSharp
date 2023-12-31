﻿namespace Events.Ticket;

public interface IGenerateTicketEvent
{
    public Guid TicketId { get; }
    public string Title { get; }
    public string Email { get; }
    public DateTime RequireDate { get; }
    public int Age { get; }
    public string Location { get; }
    public string TicketNumber { get; } 
}