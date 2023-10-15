using AutoMapper;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;
using BusBookTicket.DiscountManager.DTOs.Requests;
using BusBookTicket.DiscountManager.DTOs.Responses;
using BusBookTicket.DiscountManager.Repositories;

namespace BusBookTicket.DiscountManager.Services;

public class DiscountService : IDiscountService
{
    private readonly IMapper _mapper;
    private readonly IDiscountRepository _discountRepository;

    public DiscountService(IMapper mapper, IDiscountRepository discountRepository)
    {
        this._mapper = mapper;
        this._discountRepository = discountRepository;
    }
    public DiscountResponse getByID(int id)
    {
        Discount discount = _discountRepository.getByID(id);
        return _mapper.Map<DiscountResponse>(discount);
    }

    public List<DiscountResponse> getAll()
    {
        List<Discount> discounts = _discountRepository.getAll();
        List<DiscountResponse> responses = new List<DiscountResponse>();

        responses = AppUtils.MappObject<Discount, DiscountResponse>(discounts, _mapper);
        return responses;
    }

    public bool update(DiscountUpdate entity, int id)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        discount.discountID = id;

        return _discountRepository.update(discount);
    }

    public bool delete(int id)
    {
        Discount discount = _discountRepository.getByID(id);
        discount.status = (int)EnumsApp.Delete;
        return _discountRepository.delete(discount);
    }

    public bool create(DiscountCreate entity)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        return _discountRepository.create(discount);
    }
}