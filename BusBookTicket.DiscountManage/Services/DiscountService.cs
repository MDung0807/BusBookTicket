using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.DiscountManage.DTOs.Requests;
using BusBookTicket.DiscountManage.DTOs.Responses;
using BusBookTicket.DiscountManage.Specification;

namespace BusBookTicket.DiscountManage.Services;

public class DiscountService : IDiscountService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Discount> _repository;
    public DiscountService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._repository = unitOfWork.GenericRepository<Discount>();
    }
    public async Task<DiscountResponse> GetById(int id)
    {
        DiscountSpecification discountSpecification = new DiscountSpecification(id);
        Discount discount = await _repository.Get(discountSpecification);
        return _mapper.Map<DiscountResponse>(discount);
    }

    public async Task<List<DiscountResponse>> GetAll()
    {
        DiscountSpecification discountSpecification = new DiscountSpecification();
        List<Discount> discounts = await _repository.ToList(discountSpecification);

        List<DiscountResponse> responses = await AppUtils.MapObject<Discount, DiscountResponse>(discounts, _mapper);
        return responses;
    }

    public async Task<bool> Update(DiscountUpdate entity, int id, int userId)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        discount.Id = id;
        await _repository.Update(discount, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        DiscountSpecification discountSpecification = new DiscountSpecification(id);
        Discount discount = await _repository.Get(discountSpecification);
        discount.Status = (int)EnumsApp.Delete;
        await _repository.Delete(discount, userId);
        return true;
    }

    public async Task<bool> Create(DiscountCreate entity, int userId)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        entity.dateCreate = DateTime.Now;
        await _repository.Create(discount, userId);
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

    public async Task<bool> ChangeToWaiting(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatus(List<int> ids, int userId)
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

    public Task<object> GetAllByAdmin(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<object> GetAll(object pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<DiscountResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }
}