using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Paging;

namespace BusBookTicket.PriceManage.Services;

public interface IPriceService : IService<PriceCreate, PriceUpdate, int,PriceResponse, PricePaging, PricePagingResult>
{
    
}