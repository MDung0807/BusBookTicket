namespace BusBookTicket.Ticket.DTOs.Response;

public class TicketResponse
{
    public int Id { get; set; }
    public int Status { get; set; }
    public DateTime Date { get; set; }
    public string BusNumber { get; set; }
    public string BusId { get; set; }
    public string Introduction { get; set; }
    public string Company { get; set; }
    public string CompanyLogo { get; set; }
    public string BusType { get; set; }
    public int TotalEmptySeat { get; set; }
    public List<StationResponse> ListStation { get; set; }
    public List<TicketItemResponse> ItemResponses { get; set; }
}

public class StationResponse
{
    public int TicketRouteDetailId { get; set; }
    public string Station { get; set; }
    public int IndexStation { get; set; }
    public int DiscountPrice { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Address { get; set; }
}