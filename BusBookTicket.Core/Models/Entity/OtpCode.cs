namespace BusBookTicket.Core.Models.Entity;

public class OtpCode : BaseEntity
{
    public string Email { get; set; }
    public string Code { get; set; }
    public int UserId { get; set; }
}