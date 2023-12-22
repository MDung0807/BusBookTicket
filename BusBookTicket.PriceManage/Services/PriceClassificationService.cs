using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.DTOs.Requests;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Paging;
using BusBookTicket.PriceManage.Specification;

namespace BusBookTicket.PriceManage.Services;

public class PriceClassificationService : IPriceClassificationService
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenericRepository<PriceClassification> _repository;

    public PriceClassificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<PriceClassification>();
        _mapper = mapper;
    }
    public Task<PriceClassificationResponse> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<PriceClassificationResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(PriceClassificationUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Create(PriceClassificationCreate entity, int userId)
    {
        PriceClassification priceClassification = _mapper.Map<PriceClassification>(entity);
        priceClassification.Status = (int)EnumsApp.Waiting;
        priceClassification.Company = new Company
        {
            Id = userId
        };
        await _repository.Create(priceClassification, userId);
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

    public Task<PriceClassificationPagingResult> GetAllByAdmin(PriceClassificationPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PriceClassificationPagingResult> GetAll(PriceClassificationPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<PriceClassificationPagingResult> GetAll(PriceClassificationPaging pagingRequest, int idMaster)
    {
        PriceClassificationSpecification specification =
            new PriceClassificationSpecification(companyId: idMaster, checkStatus: false, paging: pagingRequest);

        int count = await _repository.Count(
            new PriceClassificationSpecification(companyId: idMaster, checkStatus: false));
        List<PriceClassification> priceClassifications = await _repository.ToList(specification: specification);
        var responses =
            await AppUtils.MapObject<PriceClassification, PriceClassificationResponse>(priceClassifications, _mapper);
        var result = AppUtils.ResultPaging<PriceClassificationPagingResult, PriceClassificationResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count,
            items: responses);
        return result;
    }
    

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }
}