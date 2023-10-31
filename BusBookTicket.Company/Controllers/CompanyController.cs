using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Services;
using BusBookTicket.CompanyManage.Utils;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.CompanyManage.Controllers;

[ApiController]
[Route("api/company")]
public class CompanyController : ControllerBase
{
    #region -- Properties --
    private readonly ICompanyServices _companyServices;
    #endregion

    public CompanyController(ICompanyServices companyServices)
    {
        this._companyServices = companyServices;
    }

    #region -- Controllers --

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> register([FromForm] FormRegisterCompany request)
    {
        request.roleName = AppConstants.COMPANY;
        await _companyServices.create(request);
        return Ok(new Response<string>(false, CompanyConstants.SUCCESS));
    }

    [HttpPut("update")]
    [Authorize(Roles = ("COMPANY"))]
    public async Task<IActionResult> update([FromBody] FormUpdateCompany request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        bool status = await _companyServices.update(request, id);
        return Ok(new Response<string>(!status, "Response"));
    }
    
    [HttpPut("admin/update")]
    [Authorize(Roles = ("ADMIN"))]
    public async Task<IActionResult> updateByAdmin([FromBody] FormUpdateCompany request)
    {
        bool status = await _companyServices.update(request, request.companyID);
        return Ok(new Response<string>(!status, "Response"));
    }

    [HttpGet("admin/active")]
    [Authorize(Roles = ("ADMIN"))]
    public async Task<IActionResult> active([FromQuery] int id)
    {
        bool status = await _companyServices.changeStatus(id, (int)EnumsApp.Active);
        return Ok(new Response<string>(!status, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = ("COMPANY,ADMIN"))]
    public async Task<IActionResult> delete(int id)
    {
        bool status = await _companyServices.delete(id);
        return Ok(new Response<string>(!status, ""));
    }

    [AllowAnonymous]
    [HttpGet("get")]
    public async Task<IActionResult> getById(int id)
    {
        ProfileCompany response = await _companyServices.getByID(id);
        return Ok(new Response<ProfileCompany>(false, response));
    }

    [AllowAnonymous]
    [HttpGet("getAll")]
    public async Task<IActionResult> getAllCompany()
    {
        List<ProfileCompany> responses = await _companyServices.getAll();
        return Ok(new Response<List<ProfileCompany>>(false, responses));
    }
    #endregion
}