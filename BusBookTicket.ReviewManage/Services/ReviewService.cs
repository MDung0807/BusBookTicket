using AutoMapper;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Services.Bills;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.ReviewManage.DTOs.Requests;
using BusBookTicket.ReviewManage.DTOs.Responses;
using BusBookTicket.ReviewManage.Paging;
using BusBookTicket.ReviewManage.Specifications;
using BusBookTicket.ReviewManage.Utils;

namespace BusBookTicket.ReviewManage.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Review> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBillService _billService;

        public ReviewService(IMapper mapper, IUnitOfWork unitOfWork, IBillService billService)
        {
            _repository = unitOfWork.GenericRepository<Review>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _billService = billService;
        }

        #region -- Public Method --

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

        public async Task<bool> Create(ReviewRequest entity, int userId)
        {
            if (! await CheckBillStatus(busId: entity.BusId, userId))
            {
                throw new ExceptionDetail(ReviewConstants.UNAUTHOR);
            }
            Review review = _mapper.Map<Review>(entity);
            review.Customer ??= new Customer();
            review.Customer.Id = userId;
            review.Status = (int)EnumsApp.Active;
            await _repository.Create(review, userId);
            return true;
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

        public async Task<ReviewPagingResult> GetAll(ReviewPaging pagingRequest, int idMaster, bool checkStatus = false)
        {
            ReviewSpecification specification = new ReviewSpecification(idMaster, paging: pagingRequest);
            int count = await _repository.Count(new ReviewSpecification(idMaster));
            List<Review> reviews = await _repository.ToList(specification);
            List<ReviewResponse> responses = await AppUtils.MapObject<Review, ReviewResponse>(reviews, _mapper);
            ReviewPagingResult result = AppUtils.ResultPaging<ReviewPagingResult, ReviewResponse>(
                pagingRequest.PageIndex, pagingRequest.PageSize, count, responses);
            return result;
        }

        public Task<bool> DeleteHard(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<float> GetRateAverage(int busId)
        {
            ReviewSpecification specification = new ReviewSpecification(busId:busId);
            List<Review> reviews = await _repository.ToList(specification);
            float rateAverage = 0;
            foreach (var item in reviews)
            {
                rateAverage += item.Rate;
            }

            if (reviews.Count != 0)
            {
                rateAverage /= reviews.Count();
            }

            return rateAverage;
        }

        #endregion -- Public Method --

        #region -- Private Method --

        private async Task<bool> CheckBillStatus(int busId, int userId)
        {
            BillResponse response = await _billService.GetBillByUserAndBus(userId: userId, busId: busId);
            if (response == null)
            {
                return false;
            }

            return true;
        }
        #endregion -- Private Method --
    }
}
