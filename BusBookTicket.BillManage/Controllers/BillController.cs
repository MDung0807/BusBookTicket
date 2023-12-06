using BusBookTicket.Auth.Security;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Paging;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Services.Bills;
using BusBookTicket.BillManage.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.BillManage.Controllers;

[ApiController]
[Route("api/bill")]
public class BillController : ControllerBase
{
    private readonly IBillService _billService;
    private readonly IBillItemService _billItemService;

    public BillController(IBillService billService, IBillItemService billItemService)
    {
        this._billService = billService;
        this._billItemService = billItemService;
    }

    #region -- Controller --
    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateBill([FromBody] BillRequest billRequest)
    {
        var validator = new BillRequestValidator();
        var result = await validator.ValidateAsync(billRequest);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        await _billService.Create(billRequest, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getBill")]
    public async Task<IActionResult> GetBill(int id)
    {
        BillResponse billResponse = await _billService.GetById(id);
        return Ok(new Response<BillResponse>(false, billResponse));
    }
    
    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllBill([FromQuery] BillPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        BillPagingResult billResponse = await _billService.GetAllBillInUser(paging, userId);
        return Ok(new Response<BillPagingResult>(false, billResponse));
    }
    #endregion -- Controller --

}