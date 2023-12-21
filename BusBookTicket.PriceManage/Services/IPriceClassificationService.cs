using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Paging;

namespace BusBookTicket.PriceManage.Services;

public interface IPriceClassificationService : IService<PriceClassificationCreate, PriceClassificationUpdate, int, PriceClassificationResponse, PriceClassificationPaging, PriceClassificationPagingResult>
{
    
}