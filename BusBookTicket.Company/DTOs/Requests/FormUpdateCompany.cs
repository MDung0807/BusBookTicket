using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CompanyManage.DTOs.Requests;

[ValidateNever]
public class FormUpdateCompany
{
    public int companyID { get; set; }
    public string? name { get; set; }
    public string? introduction { get; set; }
    public string? email { get; set; }
    public string? phoneNumber { get; set; }
    public int status { get; set; }
}