using BusBookTicket.Auth.Security;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.BusStationManage.Utils;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.BusStationManage.Controllers
{
    [ApiController]
    [Route("api/stations")]
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
        public async Task<IActionResult> GetAll()
        {
            List<BusStationResponse> responses = await _busStationService.GetAll();
            return Ok(new Response<List<BusStationResponse>>(false, responses));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            BusStationResponse response = await _busStationService.GetById(id);
            return Ok(new Response<BusStationResponse>(false, response));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPut("admin/update")]
        public async Task<IActionResult> Update([FromBody] BST_FormUpdate request)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Update(request, request.Id, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> CreateByAdmin([FromBody] BST_FormCreate request)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Create(request, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("admin/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Delete(id, userId);
            return Ok(new Response<string>(!status, "Response"));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/active")]
        public async Task<IActionResult> Active(int id)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.ChangeIsActive(id, userId);
            return Ok(new Response<string>(!status, BusStationConstants.SUSSCESS));
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/disable")]
        public async Task<IActionResult> Disable(int id)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.ChangeToDisable(id, userId);
            return Ok(new Response<string>(!status, BusStationConstants.SUSSCESS));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/waiting")]
        public async Task<IActionResult> ChangeIsWaiting(int id)
        {
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.ChangeToWaiting(id, userId);
            return Ok(new Response<string>(!status, BusStationConstants.SUSSCESS));
        }
        
        [Authorize(Roles = "COMPANY")]
        [HttpPost("company/create")]
        public async Task<IActionResult> CreateByCompany([FromBody] BST_FormCreate request)
        {
            request.Status = (int)EnumsApp.Waiting;
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Create(request, userId);
            return Ok(new Response<string>(!status, BusStationConstants.SUSSCESS));
        }

        [HttpGet("getByName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStationByName([FromQuery]string name)
        {
            BusStationResponse response = await _busStationService.getStationByName(name);
            return Ok(new Response<BusStationResponse>(false, response));
        }
        
        [HttpGet("getByLocation")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStationByLocation([FromQuery] string location)
        {
            List<BusStationResponse> responses = await _busStationService.getStationByLocation(location);
            return Ok(new Response<List<BusStationResponse>>(false, responses));
        }
        #endregion -- Controllers --
    }
}
