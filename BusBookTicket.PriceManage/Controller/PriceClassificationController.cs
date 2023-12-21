using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.Paging;
using BusBookTicket.PriceManage.Services;
using BusBookTicket.PriceManage.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.PriceManage.Controller;

[ApiController]
[Route("api/PriceClassification")]
public class PriceClassificationController : ControllerBase
{
    private readonly IPriceClassificationService _service;

    public PriceClassificationController(IPriceClassificationService service)
    {
        _service = service;
    }

    #region -- Controller --

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] PriceClassificationCreate request)
    {
        var validator = new PriceClassificationCreateValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }

        int userId = JwtUtils.GetUserID(HttpContext);
        var status = await _service.Create(request, userId);
        return Ok(new Response<string>(!status, AppConstants.SUCCESS));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpPost("getInCompany")]
    public async Task<IActionResult> GetInCompany([FromQuery] PriceClassificationPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _service.GetAll(paging, userId);
        return Ok(new Response<PriceClassificationPagingResult>(false,result));
    }
    #endregion -- Controller --
}