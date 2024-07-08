namespace BusBookTicket.Ticket.Repositories;

public interface ITicketRepository
{
    Task<double> TotalTicketInMonth(int companyId, int month, int year);
}