namespace BusBookTicket.Application.Notification.Modal;

public class NotificationResponse
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string Actor { get; set; }
    public string Href { get; set; }
    public string Sender { get; set; }
    public string Avatar { get; set; }
    public int Status { get; set; }
    public DateTime DateCreate { get; set; }
}