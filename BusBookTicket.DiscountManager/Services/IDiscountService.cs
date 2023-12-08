
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;

namespace BusBookTicket.DiscountManager.Services;

public interface IDiscountService : IService<DiscountCreate, DiscountUpdate, int, DiscountResponse, object, object>
{
    
}