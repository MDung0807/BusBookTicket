using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;
using BusBookTicket.DiscountManager.Specification;

namespace BusBookTicket.DiscountManager.Services;

public class DiscountService : IDiscountService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Discount> _repository;
    public DiscountService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
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
        List<DiscountResponse> responses = new List<DiscountResponse>();

        responses = await AppUtils.MappObject<Discount, DiscountResponse>(discounts, _mapper);
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
}