
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.DiscountManage.DTOs.Requests;
using BusBookTicket.DiscountManage.DTOs.Responses;

namespace BusBookTicket.DiscountManage.Services;

public interface IDiscountService : IService<DiscountCreate, DiscountUpdate, int, DiscountResponse, object, object>
{
    
}