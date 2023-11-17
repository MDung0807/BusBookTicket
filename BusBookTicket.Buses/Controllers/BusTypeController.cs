using BusBookTicket.Auth.Security;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/bustype")]
public class BusTypeController : ControllerBase
{
    private readonly IBusTypeService _busTypeService;

    public BusTypeController(IBusTypeService busTypeService)
    {
        _busTypeService = busTypeService;
    }

    #region -- Controllers --
    
        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            List<BusTypeResponse> responses = await _busTypeService.GetAll();
            return Ok(new Response<List<BusTypeResponse>>(false, responses));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            BusTypeResponse response = await _busTypeService.GetById(id);
            return Ok(new Response<BusTypeResponse>(false, response));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("admin/update")]
        public async Task<IActionResult> Update([FromBody] BusTypeFormUpdate request)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busTypeService.Update(request, request.BusTypeId, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> CreateByAdmin([FromBody] BusTypeForm request)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busTypeService.Create(request, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("admin/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busTypeService.Delete(id, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "COMPANY")]
        [HttpPost("company/create")]
        public async Task<IActionResult> CreateByCompany([FromBody] BusTypeForm request)
        {
            request.Status = 0;
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busTypeService.Create(request, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        #endregion -- Controllers --
}