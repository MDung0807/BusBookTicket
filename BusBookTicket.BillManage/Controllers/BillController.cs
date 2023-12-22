using BusBookTicket.Auth.Security;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Paging;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Services.Bills;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.BillManage.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.BillManage.Controllers;

[ApiController]
[Route("api/bills")]
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

    [HttpPut("changeCompleteStatus")]
    [Authorize(Roles = AppConstants.COMPANY)]
    public async Task<IActionResult> ChangeCompleteStatus([FromQuery] int billId)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _billService.ChangeCompleteStatus(billId, userId);
        return Ok(new Response<string>(!status, BillConstants.SUCCESS));
    }
    
    [HttpPut("changeIsDelete")]
    [Authorize(Roles = AppConstants.CUSTOMER)]
    public async Task<IActionResult> ChangeIsDelete([FromQuery] int id)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        bool status = await _billService.Delete(id, userId);
        return Ok(new Response<string>(!status, BillConstants.SUCCESS));
    }
    
    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getAllInWaitingStatus")]
    public async Task<IActionResult> GetAllInWaitingStatus([FromQuery] BillPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        BillPagingResult billResponse = await _billService.GetAllInWaitingStatus(paging, userId);
        return Ok(new Response<BillPagingResult>(false, billResponse));
    }
    
    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getAllInDeleteStatus")]
    public async Task<IActionResult> GetAllInDeleteStatus([FromQuery] BillPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        BillPagingResult billResponse = await _billService.GetAllInDeleteStatus(paging, userId);
        return Ok(new Response<BillPagingResult>(false, billResponse));
    }
    
    [Authorize(Roles = "CUSTOMER")]
    [HttpGet("getAllInCompleteStatus")]
    public async Task<IActionResult> GetAllInCompleteStatus([FromQuery] BillPaging paging)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        BillPagingResult billResponse = await _billService.GetAllInCompleteStatus(paging, userId);
        return Ok(new Response<BillPagingResult>(false, billResponse));
    }

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("revenueStatistics")]
    public async Task<IActionResult> RevenueStatistics([FromQuery] int year)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        object result = await _billService.RevenueStatistics(userId, year);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("revenueStatisticsByQuarter")]
    public async Task<IActionResult> RevenueStatisticsByQuarter([FromQuery] int year)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        object result = await _billService.RevenueStatistics(userId, year);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.ADMIN)]
    [HttpGet("getStatisticsStationByAdmin")]
    public async Task<IActionResult> GetStatisticsStationByAdmin([FromQuery] int year, int take, bool desc)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        object result = await _billService.GetStatisticsStationByAdmin(year, take, desc);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("getStatisticsStationByCompany")]
    public async Task<IActionResult> GetStatisticsStationByCompany([FromQuery] int year, int take, bool desc)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        object result = await _billService.GetStatisticsStationByCompany(userId, year, take, desc);
        return Ok(new Response<object>(false, result));
    }
    #endregion -- Controller --
}