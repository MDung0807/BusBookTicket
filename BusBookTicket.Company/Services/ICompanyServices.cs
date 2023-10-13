using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Common.Common;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;

namespace BusBookTicket.CompanyManage.Services;

public interface ICompanyServices : IService<FormRegisterCompany, FormUpdateCompany, int, ProfileCompany>
{
}