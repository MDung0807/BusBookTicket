using BusBookTicket.Core.Common;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;
using BusBookTicket.DiscountManager.Services;

namespace BusBookTicket.DiscountManager.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/discount")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService service)
    {
        this._discountService = service;
    }

    #region -- Controllers --

    [HttpGet("get")]
    [AllowAnonymous]
    public async Task<IActionResult> getByID(int id)
    {
        DiscountResponse response = await _discountService.getByID(id);
        return Ok(new Response<DiscountResponse>(false, response));
    }
    
    [HttpGet("getAll")]
    [AllowAnonymous]
    public async Task<IActionResult> getAll()
    {
        List<DiscountResponse> responses = await _discountService.getAll();
        return Ok(new Response<List<DiscountResponse>>(false, responses));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> create([FromBody] DiscountCreate discountCreate)
    {
        bool status = await _discountService.create(discountCreate);
        return Ok(new Response<string>(!status, "Response"));
    }
    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> update([FromBody] DiscountUpdate discountUpdate)
    {
        bool status = await _discountService.update(discountUpdate, discountUpdate.discountID);
        return Ok(new Response<string>(!status, "response"));
    }
    [HttpDelete("delete")]
    [AllowAnonymous]
    public async Task<IActionResult> delete(int id)
    {
        bool status = await _discountService.delete(id);
        return Ok(new Response<string>(!status, "response"));
    }

    #endregion -- Controllers --
}