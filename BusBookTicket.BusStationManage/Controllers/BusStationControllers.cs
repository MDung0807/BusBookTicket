using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.Common.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.BusStationManage.Controllers
{
    [ApiController]
    [Route("api/busstation")]
    public class BusStationController : ControllerBase
    {
        private readonly IBusStationService _busStationService;

        public BusStationController(IBusStationService busStationService)
        {
            this._busStationService = busStationService;
        }

        #region -- Controllers --

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<IActionResult> getAll()
        {
            List<BusStationResponse> responses = await _busStationService.getAll();
            return Ok(new Response<List<BusStationResponse>>(false, responses));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getByID(int id)
        {
            BusStationResponse response = await _busStationService.getByID(id);
            return Ok(new Response<BusStationResponse>(false, response));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("admin/update")]
        public async Task<IActionResult> update([FromBody] BST_FormUpdate request)
        {
             bool status = await _busStationService.update(request, request.busStationID);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> createByAdmin([FromBody] BST_FormCreate request)
        {
            bool status = await _busStationService.create(request);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("admin/delete")]
        public async Task<IActionResult> delete(int id)
        {
            bool status = await _busStationService.delete(id);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "COMPANY")]
        [HttpPost("company/create")]
        public async Task<IActionResult> createByCompany([FromBody] BST_FormCreate request)
        {
            request.status = 0;
            bool status = await _busStationService.create(request);
            return Ok(new Response<string>(!status, "Response"));
        }
        #endregion -- Controllers --
    }
}
