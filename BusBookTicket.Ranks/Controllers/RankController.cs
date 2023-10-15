using BusBookTicket.Common.Common;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Services;
using BusBookTicket.Ranks.Utilities;

namespace BusBookTicket.Ranks.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/rank")]
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
    public IActionResult create([FromBody] RankCreate request)
    {
        bool status = _rankService.create(request);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpPatch("update")]
    public IActionResult update([FromBody] RankUpdate request)
    {
        bool status = _rankService.update(request, request.rankID);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpDelete("delete")]
    public IActionResult delete(int id)
    {
        bool status = _rankService.delete(id);
        return Ok(new Response<string>(false, RankConstants.SUCCESS));
    }
    
    [HttpGet("get")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult get(int id)
    {
        RankResponse response = _rankService.getByID(id);
        return Ok(new Response<RankResponse>(false, response));
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("getAll")]
    public IActionResult getAll()
    {
        List<RankResponse> responses = _rankService.getAll();
        return Ok(new Response<List<RankResponse>>(false, responses));
    }
    #endregion
}