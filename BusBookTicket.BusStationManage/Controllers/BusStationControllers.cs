using BusBookTicket.Auth.Security;
using BusBookTicket.BusStationManage.DTOs.Requests;
using BusBookTicket.BusStationManage.DTOs.Responses;
using BusBookTicket.BusStationManage.Paging;
using BusBookTicket.BusStationManage.Services;
using BusBookTicket.BusStationManage.Utils;
using BusBookTicket.BusStationManage.Validator;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
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
        public async Task<IActionResult> GetAll([FromQuery] StationPaging request)
        {
            StationPagingResult response = await _busStationService.GetAll(request);
            return Ok(new Response<StationPagingResult>(false, response));
        }
        
        [HttpGet("admin/getAll")]
        [Authorize(Roles = AppConstants.ADMIN)]
        public async Task<IActionResult> GetAllByAdmin([FromQuery] StationPaging paging)
        {
            StationPagingResult responses = await _busStationService.GetAllByAdmin(paging);
            return Ok(new Response<StationPagingResult>(false, responses));
        }
        [HttpGet("admin/find")]
        [Authorize(Roles = AppConstants.ADMIN)]
        public async Task<IActionResult> Find([FromQuery] string param ,[FromQuery] StationPaging paging)
        {
            StationPagingResult responses = await _busStationService.FindByParam(param, paging);
            return Ok(new Response<StationPagingResult>(false, responses));
        }
        
        [HttpGet("getAllInBus")]
        [AllowAnonymous]        
        public async Task<IActionResult> GetAllStationInBus([FromQuery] int busId, [FromQuery] StationPaging paging)
        {
            StationPagingResult responses = await _busStationService.GetAllStationInBus(busId, paging);
            return Ok(new Response<StationPagingResult>(false, responses));
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
            var validator = new BST_FormUpdateValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Update(request, request.Id, userId);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public async Task<IActionResult> CreateByAdmin([FromBody] BST_FormCreate request)
        {
            var validator = new BST_FormCreateValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }

            request.Status = (int)EnumsApp.Active;
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
        [HttpPost("companies/create")]
        public async Task<IActionResult> CreateByCompany([FromBody] BST_FormCreate request)
        {
            var validator = new BST_FormCreateValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                throw new ValidatorException(result.Errors);
            }
            request.Status = (int)EnumsApp.Waiting;
            int userId = JwtUtils.GetUserID(HttpContext);
            bool status = await _busStationService.Create(request, userId);
            return Ok(new Response<string>(!status, BusStationConstants.SUSSCESS));
        }

        [HttpGet("getByName")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStationByName([FromQuery]string name)
        {
            BusStationResponse response = await _busStationService.GetStationByName(name);
            return Ok(new Response<BusStationResponse>(false, response));
        }
        
        [HttpGet("getByLocation")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStationByLocation([FromQuery] string location, [FromQuery] StationPaging paging)
        {
            StationPagingResult responses = await _busStationService.GetStationByLocation(location, paging);
            return Ok(new Response<StationPagingResult>(false, responses));
        }
        #endregion -- Controllers --
    }
}
