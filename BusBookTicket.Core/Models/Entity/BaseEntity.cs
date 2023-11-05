namespace BusBookTicket.Core.Models.Entity;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
    public int UpdateBy { get; set; }
    public int CreateBy { get; set; }
    public int Status { get; set; }
}