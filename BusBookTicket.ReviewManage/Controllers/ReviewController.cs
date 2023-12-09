using BusBookTicket.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.Review.Controllers;

[ApiController]
[Route("api/tickets")]
public class ReviewController : ControllerBase
{
    public ReviewController(){}

    #region -- Controller --

    [HttpGet]
    [Authorize(Roles = "COMPANY")]
    public IActionResult GetAll()
    {
        return Ok (new Response<string>(false, "fád"));
    }
    

    #endregion -- Controller --
}