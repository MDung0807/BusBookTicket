﻿using BusBookTicket.Application.PayPalPayment.Services;
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
    private readonly PaypalClient _paypalClient;

    public BillController(IBillService billService, IBillItemService billItemService, PaypalClient paypalClient)
    {
        
        this._billService = billService;
        this._billItemService = billItemService;
        _paypalClient = paypalClient;
    }

    #region -- Controller --
    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("reserve")]
    public async Task<IActionResult> Reserve([FromBody] BillRequest billRequest)
    {
        var validator = new BillRequestValidator();
        var result = await validator.ValidateAsync(billRequest);
        if (!result.IsValid)
        {
            throw new ValidatorException(result.Errors);
        }
        int userId = JwtUtils.GetUserID(HttpContext);
        // await _billService.Create(billRequest, userId);
        // Reserve
        await _billService.Reserve(billRequest, userId);
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

    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("book")]
    public async Task<IActionResult> Book(CancellationToken cancellationToken )
    {
        try
        {
            // set the transaction price and currency
            var price = "5.00";
            var currency = "USD";

            // "reference" is the transaction key
            var reference = "INV001";

            var response = await _paypalClient.CreateOrder(price, currency, reference);

            return Ok(response);
        }
        catch (Exception e)
        {
            var error = new
            {
                e.GetBaseException().Message
            };

            return BadRequest(error);
        }
    }
    
    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("Capture")]
    public async Task<IActionResult> Capture(string orderId, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _paypalClient.CaptureOrder(orderId);

            var reference = response.purchase_units[0].reference_id;

            // Put your logic to save the transaction here
            // You can use the "reference" variable as a transaction key

            return Ok(response);
        }
        catch (Exception e)
        {
            var error = new
            {
                e.GetBaseException().Message
            };

            return BadRequest(error);
        }
    }
    
    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("paymentPaypal")]
    public async Task<IActionResult> PaypalPayment([FromBody] BillRequest billRequest)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        // Check Reserve is
        var reserve = await _billService.CheckReserve(request: billRequest, userId);
        if (!reserve)
            return NotFound(new Response<string>(true, "Not found your reserve"));
        var result = await _billService.PaymentPaypal(billRequest, userId);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpPost("paymentDirect")]
    public async Task<IActionResult> PaymentDirect([FromBody] BillRequest billRequest)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        
        // Check Reserve is
        var reserve = await _billService.CheckReserve(request: billRequest, userId);
        if (!reserve)
            return Ok(new Response<string>(true, "Not found your reserve"));
        
        await _billService.Create(billRequest, userId);
        return Ok(new Response<string>(false, "Response"));
    }

    [Authorize(Roles = AppConstants.CUSTOMER)]
    [HttpGet("CheckReserve")]
    public async Task<IActionResult> CheckReserve(BillRequest request)
    {
        int userId = JwtUtils.GetUserID(HttpContext);
        var result = await _billService.CheckReserve(request, userId);
        return Ok(new Response<bool>(false, result));
    }

    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("totalBill")]
    public async Task<IActionResult> GetTotalBill()
    {
        int companyId = JwtUtils.GetUserID(HttpContext);
        var result = await _billService.TotalBill(companyId);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("Sales")]
    public async Task<IActionResult> Sales()
    {
        int companyId = JwtUtils.GetUserID(HttpContext);
        var result = await _billService.Sales(companyId);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("company/statistical")]
    public async Task<IActionResult> GetStatistical([FromQuery]DateOnly dateOnly)
    {
        int companyId = JwtUtils.GetUserID(HttpContext);
        var result = await _billService.Statistical(companyId, dateOnly.Month, dateOnly.Year);
        return Ok(new Response<object>(false, result));
    }
    
    [Authorize(Roles = AppConstants.COMPANY)]
    [HttpGet("topRoute")]
    public async Task<IActionResult> GetTopRouteInBill([FromQuery] int top)
    {
        if (top == default)
            top = 5;
        int companyId = JwtUtils.GetUserID(HttpContext);
        var result = await _billService.TopRouteInBill(companyId, top);
        return Ok(new Response<object>(false, result));
    }

    [Authorize(Roles = AppConstants.ADMIN)]
    [HttpGet("admin/TopRoute")]
    public async Task<IActionResult> GetTopRoute([FromQuery] int top)
    {
        if (top == default)
            top = 5;
        var result = await _billService.TopRouteInBill( top);
        return Ok(new Response<object>(false, result));
    }
    #endregion -- Controller --
}