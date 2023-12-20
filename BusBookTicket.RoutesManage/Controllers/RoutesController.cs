using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.RoutesManage.DTOs.Requests;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Paging;
using BusBookTicket.RoutesManage.Service;
using BusBookTicket.RoutesManage.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.RoutesManage.Controllers;


[ApiController]
[Route("api/routes")]
public class RoutesController : ControllerBase
{
    private IRoutesService _service;

    public RoutesController(IRoutesService service)
    {
        _service = service;
    }

    #region -- Controller --

    [Authorize(Roles = AppConstants.ADMIN)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRoute([FromBody] RoutesCreate request)
    {
        
        var validator = new RouteValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        
        int userId = JwtUtils.GetUserID(HttpContext);
        var status = await _service.Create(request, userId);
        return Ok(new Response<string>(!status, AppConstants.SUCCESS));
    }
    
    [AllowAnonymous]
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAll([FromQuery] RoutesPaging paging)
    {
        var responses = await _service.GetAll();
        return Ok(new Response<List<RoutesResponse>>(false, responses));
    }
    #endregion -- Controller --
} 
