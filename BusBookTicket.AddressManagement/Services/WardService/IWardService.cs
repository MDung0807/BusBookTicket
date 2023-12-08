using BusBookTicket.AddressManagement.DTOs.Requests.Ward;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.AddressManagement.Services.WardService;

public interface IWardService : IService<WardCreate, WardUpdate, int, WardResponse, object, object>
{
    Task<Ward> WardGet(int id);
}