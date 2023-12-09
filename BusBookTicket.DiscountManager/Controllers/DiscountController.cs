using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;
using BusBookTicket.DiscountManager.Services;

namespace BusBookTicket.DiscountManager.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/discounts")]
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
        DiscountResponse response = await _discountService.GetById(id);
        return Ok(new Response<DiscountResponse>(false, response));
    }
    
    [HttpGet("getAll")]
    [AllowAnonymous]
    public async Task<IActionResult> getAll()
    {
        List<DiscountResponse> responses = await _discountService.GetAll();
        return Ok(new Response<List<DiscountResponse>>(false, responses));
    }

    [HttpPost("create")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> create([FromBody] DiscountCreate discountCreate)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _discountService.Create(discountCreate, userId);
        return Ok(new Response<string>(!status, "Response"));
    }
    [HttpPut("update")]
    [Authorize(Roles = "COMPANY")]
    public async Task<IActionResult> update([FromBody] DiscountUpdate discountUpdate)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _discountService.Update(discountUpdate, discountUpdate.discountID, userId);
        return Ok(new Response<string>(!status, "response"));
    }
    [HttpDelete("delete")]
    [AllowAnonymous]
    public async Task<IActionResult> delete(int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _discountService.Delete(id, userId);
        return Ok(new Response<string>(!status, "response"));
    }

    #endregion -- Controllers --
}