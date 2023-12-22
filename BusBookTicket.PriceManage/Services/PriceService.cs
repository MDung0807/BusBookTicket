using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Paging;
using BusBookTicket.PriceManage.Specification;

namespace BusBookTicket.PriceManage.Services;

public class PriceService : IPriceService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Prices> _repository;

    public PriceService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = _unitOfWork.GenericRepository<Prices>();
    }
    
    public Task<PriceResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PriceResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(PriceUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(PriceCreate entity, int userId)
    {
        Prices price = _mapper.Map<Prices>(entity);
        price.Company.Id = userId;
        await _repository.Create(price, userId: userId);
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

    public Task<PricePagingResult> GetAllByAdmin(PricePaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PricePagingResult> GetAll(PricePaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<PricePagingResult> GetAll(PricePaging pagingRequest, int idMaster)
    {
        PriceSpecification specification = new PriceSpecification(companyId:idMaster, paging: pagingRequest);
        int count = await _repository.Count(new PriceSpecification(companyId: idMaster));
        List<Prices> pricesList = await _repository.ToList(specification);

        var result = AppUtils.ResultPaging<PricePagingResult, PriceResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count: count,
            items: await AppUtils.MapObject<Prices, PriceResponse>(pricesList, _mapper));
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<PriceResponse> GetInRoute(int routeId, int companyId)
    {
        PriceSpecification specification = new PriceSpecification(routeId: routeId, companyId: companyId);
        Prices price = await _repository.Get(specification);
        return _mapper.Map<PriceResponse>(price);
    }
}