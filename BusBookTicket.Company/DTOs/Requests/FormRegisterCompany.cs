using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CompanyManage.DTOs.Requests;

[ValidateNever]
public class FormRegisterCompany
{
    #region -- Company -- 
    public string? Name { get; set; }
    public string? Introduction { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile Logo { get; set; }
    public int WardId { get; set; }
    #endregion -- Company --

    #region -- Account --
    public string username { get; set; }
    public string password { get; set; }
    public string roleName { get; set; }
    #endregion -- Account --
}