namespace BusBookTicket.CompanyManage.DTOs.Responses;

public class ProfileCompany
{
    public int companyID { get; set; }
    public string? name { get; set; }
    public string? introduction { get; set; }
    public string? email { get; set; }
    public string? phoneNumber { get; set; }
    public int status { get; set; }

    public string username { get; set; }
    public string roleName { get; set; }
}