namespace BusBookTicket.Application.MailKet.DTO.Request;

public class MailRequest
{
    public string ToMail { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string Message { get; set; }
    public string LinkImage { get; set; }
    public string FullName { get; set; }
}