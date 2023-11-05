using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.Buses.Services.SeatTypServices;

public interface ISeatTypeService : IService<SeatTypeFormCreate, SeatTypeFormUpdate, int, SeatTypeResponse>
{
    /// <summary>
    /// Get all Seat Type in Company and seat type common.
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns type="List seat type in companyID and type common (companyID = null)"></returns>
    Task<List<SeatTypeResponse>> getAll(int companyID);
}