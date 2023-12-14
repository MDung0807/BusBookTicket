namespace BusBookTicket.CompanyManage.DTOs.Responses;

public class ProfileCompany
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Introduction { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Status { get; set; }
    public string Logo { get; set; }
    public string Address { get; set; }

    public string Username { get; set; }
    public string RoleName { get; set; }
    public int WardId { get; set; }
}