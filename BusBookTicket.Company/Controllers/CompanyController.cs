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
    public async Task<IActionResult> Register([FromForm] FormRegisterCompany request)
    {
        request.RoleName = AppConstants.COMPANY;
        await _companyServices.Create(request, -1);
        return Ok(new Response<string>(false, CompanyConstants.SUCCESS));
    }

    [HttpPut("update")]
    [Authorize(Roles = ("COMPANY"))]
    public async Task<IActionResult> Update([FromBody] FormUpdateCompany request)
    {
        int id = JwtUtils.GetUserID(HttpContext);
        bool status = await _companyServices.Update(request, id, id);
        return Ok(new Response<string>(!status, "Response"));
    }
    
    [HttpPut("admin/update")]
    [Authorize(Roles = ("ADMIN"))]
    public async Task<IActionResult> UpdateByAdmin([FromBody] FormUpdateCompany request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _companyServices.Update(request, request.CompanyId, userId);
        return Ok(new Response<string>(!status, "Response"));
    }

    [HttpGet("admin/active")]
    [Authorize(Roles = ("ADMIN"))]
    public async Task<IActionResult> Active([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _companyServices.ChangeIsActive(id, userId);
        return Ok(new Response<string>(!status, "Response"));
    }

    [HttpDelete("delete")]
    [Authorize(Roles = ("COMPANY,ADMIN"))]
    public async Task<IActionResult> delete(int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _companyServices.Delete(id, userId);
        return Ok(new Response<string>(!status, ""));
    }

    [AllowAnonymous]
    [HttpGet("get")]
    public async Task<IActionResult> getById(int id)
    {
        ProfileCompany response = await _companyServices.GetById(id);
        return Ok(new Response<ProfileCompany>(false, response));
    }

    [AllowAnonymous]
    [HttpGet("getAll")]
    public async Task<IActionResult> getAllCompany()
    {
        List<ProfileCompany> responses = await _companyServices.GetAll();
        return Ok(new Response<List<ProfileCompany>>(false, responses));
    }
    #endregion
}