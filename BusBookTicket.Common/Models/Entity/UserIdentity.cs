namespace BusBookTicket.Common.Models.Entity;

public class UserIdentity
{
    public int id { get; set; }
    public string role { get; set; }

    public UserIdentity(int id, string role)
    {
        this.id = id;
        this.role = role;
    }
}