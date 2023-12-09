using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Services;
using BusBookTicket.Ranks.Utilities;

namespace BusBookTicket.Ranks.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ranks")]
public class RankController : ControllerBase
{
    private readonly IRankService _rankService;

    public RankController(IRankService rankService)
    {
        this._rankService = rankService;
    }
    
    #region -- Controllers --

    [Authorize(Roles = "ADMIN")]
    [HttpPost("create")]
    public async Task<IActionResult> create([FromBody] RankCreate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _rankService.Create(request, userId);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpPut("update")]
    public async Task<IActionResult> update([FromBody] RankUpdate request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _rankService.Update(request, request.Id, userId);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete")]
    public async Task<IActionResult> delete(int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _rankService.Delete(id, userId);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [HttpGet("get")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> get(int id)
    {
        RankResponse response = await _rankService.GetById(id);
        return Ok(new Response<RankResponse>(false, response));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("getAll")]
    public async Task<IActionResult> getAll()
    {
        List<RankResponse> responses = await _rankService.GetAll();
        return Ok(new Response<List<RankResponse>>(false, responses));
    }
    #endregion
}