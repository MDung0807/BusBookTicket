using Microsoft.AspNetCore.Mvc;

namespace BusBookTicket.BusStationManage.Controllers
{
    [Route("busstation")]
    public class BusStationControllers : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "aaa";
        }
    }
}
