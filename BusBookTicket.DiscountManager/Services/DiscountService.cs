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
    public async Task<DiscountResponse> getByID(int id)
    {
        Discount discount = await _discountRepository.getByID(id);
        return _mapper.Map<DiscountResponse>(discount);
    }

    public async Task<List<DiscountResponse>> getAll()
    {
        List<Discount> discounts = await _discountRepository.getAll();
        List<DiscountResponse> responses = new List<DiscountResponse>();

        responses = await AppUtils.MappObject<Discount, DiscountResponse>(discounts, _mapper);
        return responses;
    }

    public async Task<bool> update(DiscountUpdate entity, int id)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        discount.discountID = id;
        await _discountRepository.update(discount);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Discount discount = await _discountRepository.getByID(id);
        discount.status = (int)EnumsApp.Delete;
        await _discountRepository.delete(discount);
        return true;
    }

    public async Task<bool> create(DiscountCreate entity)
    {
        Discount discount = _mapper.Map<Discount>(entity);
        await _discountRepository.create(discount);
        return true;
    }
}