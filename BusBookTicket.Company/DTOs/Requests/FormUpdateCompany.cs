using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CompanyManage.DTOs.Requests;

[ValidateNever]
public class FormUpdateCompany
{
    public int CompanyId { get; set; }
    public string? Name { get; set; }
    public string? Introduction { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int Status { get; set; }
    public int WardId { get; set; }
}