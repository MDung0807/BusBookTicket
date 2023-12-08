using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Core.Common;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Paging;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;

namespace BusBookTicket.CompanyManage.Services;

public interface ICompanyServices : IService<FormRegisterCompany, FormUpdateCompany, int, ProfileCompany, CompanyPaging, CompanyPagingResult>
{
}