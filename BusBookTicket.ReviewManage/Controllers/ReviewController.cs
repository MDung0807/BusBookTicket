using BusBookTicket.Auth.Security;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using BusBookTicket.ReviewManage.DTOs.Requests;
using BusBookTicket.ReviewManage.Paging;
using BusBookTicket.ReviewManage.Services;
using BusBookTicket.ReviewManage.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.ReviewManage.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewController(IReviewService service)
    {
        _service = service;
    }

    #region -- Controller --

    [HttpGet("inBus")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllInBus([FromQuery] int busId, [FromQuery]ReviewPaging paging)
    {
        ReviewPagingResult result = await _service.GetAll(paging, busId);
        return Ok (new Response<ReviewPagingResult>(false, result));
    }
    
    [HttpGet("getRateAverageInBus")]
    [AllowAnonymous]
    public async Task<IActionResult> GetRateAverage([FromQuery] int busId)
    {
        float result = await _service.GetRateAverage(busId);
        return Ok (new Response<float>(false, result));
    }
        


    
    [HttpPost("create")]
    [Authorize(Roles = AppConstants.CUSTOMER)]
    public async Task<IActionResult> CreateReview([FromForm] ReviewRequest request)
    {
        var validator = new ReviewRequestValidator();
        var result = await validator.ValidateAsync(request);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }

        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _service.Create(request, userId);
        return Ok(new Response<string>(!status, ""));
    }
    #endregion -- Controller --
}