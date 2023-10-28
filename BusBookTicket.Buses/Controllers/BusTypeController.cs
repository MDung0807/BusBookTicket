using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Services.BusTypeServices;
using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Buses.Controllers;

[ApiController]
[Route("api/bystype")]
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
        public async Task<IActionResult> getAll()
        {
            List<BusTypeResponse> responses = await _busTypeService.getAll();
            return Ok(new Response<List<BusTypeResponse>>(false, responses));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getByID(int id)
        {
            BusTypeResponse response = await _busTypeService.getByID(id);
            return Ok(new Response<BusTypeResponse>(false, response));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("admin/update")]
        public async Task<IActionResult> update([FromBody] BusTypeFormUpdate request)
        {
             bool status = await _busTypeService.update(request, request.busTypeID);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> createByAdmin([FromBody] BusTypeForm request)
        {
            bool status = await _busTypeService.create(request);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("admin/delete")]
        public async Task<IActionResult> delete(int id)
        {
            bool status = await _busTypeService.delete(id);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "COMPANY")]
        [HttpPost("company/create")]
        public async Task<IActionResult> createByCompany([FromBody] BusTypeForm request)
        {
            request.status = 0;
            bool status = await _busTypeService.create(request);
            return Ok(new Response<string>(!status, "Response"));
        }
        #endregion -- Controllers --
}