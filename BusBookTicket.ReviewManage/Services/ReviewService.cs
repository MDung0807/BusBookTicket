using BusBookTicket.ReviewManage.DTOs.Requests;
using BusBookTicket.ReviewManage.DTOs.Responses;
using BusBookTicket.ReviewManage.Paging;

namespace BusBookTicket.ReviewManage.Services
{
    public class ReviewService : IReviewService
    {
        public Task<ReviewResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReviewResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(object entity, int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Create(ReviewRequest entity, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeIsActive(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeIsLock(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeToWaiting(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeToDisable(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistByParam(string param)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPagingResult> GetAllByAdmin(ReviewPaging pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPagingResult> GetAll(ReviewPaging pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewPagingResult> GetAll(ReviewPaging pagingRequest, int idMaster)
        {
            throw new NotImplementedException();
        }
    }
}
