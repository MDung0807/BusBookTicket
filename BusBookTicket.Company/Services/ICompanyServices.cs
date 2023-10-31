using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Core.Common;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;

namespace BusBookTicket.CompanyManage.Services;

public interface ICompanyServices : IService<FormRegisterCompany, FormUpdateCompany, int, ProfileCompany>
{
    Task<bool> changeStatus(int id, int stauts);
}