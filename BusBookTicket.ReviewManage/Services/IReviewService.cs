using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.ReviewManage.DTOs.Requests;
using BusBookTicket.ReviewManage.DTOs.Responses;
using BusBookTicket.ReviewManage.Paging;

namespace BusBookTicket.ReviewManage.Services
{
    public interface IReviewService : IService<ReviewRequest, object, int, ReviewResponse, ReviewPaging, ReviewPagingResult>
    {
        public Task<float> GetRateAverage(int busId);
    }
}
