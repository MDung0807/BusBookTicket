namespace BusBookTicket.CompanyManage.DTOs.Requests;

public class FormRegisterCompany
{
    #region -- Company -- 
    public string? name { get; set; }
    public string? introduction { get; set; }
    public string? email { get; set; }
    public string? phoneNumber { get; set; }
    #endregion -- Company --

    #region -- Account --
    public string username { get; set; }
    public string password { get; set; }
    public string roleName { get; set; }
    #endregion -- Account --
}