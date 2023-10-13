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
        public IActionResult getAll()
        {
            List<BusStationResponse> reponses = _busStationService.getAll();
            return Ok(new Response<List<BusStationResponse>>(false, reponses));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult getByID(int id)
        {
            BusStationResponse reponse = _busStationService.getByID(id);
            return Ok(new Response<BusStationResponse>(false, reponse));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/update")]
        public IActionResult update([FromBody] BST_FormUpdate request)
        {
             bool status = _busStationService.update(request, request.busStationID);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpPost("admin/create")]
        public IActionResult creawteByAdmin([FromBody] BST_FormCreate request)
        {
            bool status = _busStationService.create(request);
            return Ok(new Response<string>(status, "Response"));
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpGet("admin/delete")]
        public IActionResult delete(int id)
        {
            bool status = _busStationService.delete(id);
            return Ok(new Response<string>(!status, "Response"));
        }
        
        [Authorize(Roles = "COMPANY")]
        [HttpPost("company/create")]
        public IActionResult creawteByCompany([FromBody] BST_FormCreate request)
        {
            request.status = 0;
            bool status = _busStationService.create(request);
            return Ok(new Response<string>(!status, "Response"));
        }
        #endregion -- Controllers --
    }
}
