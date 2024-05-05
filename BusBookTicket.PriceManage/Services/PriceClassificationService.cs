using AutoMapper;
using BusBookTicket.Application.Notification.Services;
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
    private readonly INotificationService _notification;

    public PriceClassificationService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notification)
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<PriceClassification>();
        _mapper = mapper;
        _notification = notification;
    }
    public async Task<PriceClassificationResponse> GetById(int id)
    {
        PriceClassificationSpecification specification = new PriceClassificationSpecification(id);
        PriceClassification priceClassification = await _repository.Get(specification);
        return _mapper.Map<PriceClassificationResponse>(priceClassification);
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
        await _notification.InsertNotification();
        return true;
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        PriceClassificationSpecification specification = new PriceClassificationSpecification(id: id, checkStatus: false, getIsChangeStatus: true);
        PriceClassification priceClassification = await _repository.Get(specification, checkStatus: false);
        await _repository.ChangeStatus(priceClassification, userId: userId, (int)EnumsApp.Active);
        return true;
    }

    public Task<bool> ChangeIsLock(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeToWaiting(int id, int userId)
    {
        PriceClassificationSpecification specification = new PriceClassificationSpecification(id: id, checkStatus: false, getIsChangeStatus: true);
        PriceClassification priceClassification = await _repository.Get(specification, checkStatus: false);
        await _repository.ChangeStatus(priceClassification, userId: userId, (int)EnumsApp.Waiting);
        return true;
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

    public async Task<PriceClassificationPagingResult> GetAllByAdmin(PriceClassificationPaging pagingRequest)
    {
        PriceClassificationSpecification specification =
            new PriceClassificationSpecification(checkStatus: false, paging: pagingRequest);
        List<PriceClassification> priceClassifications = await _repository.ToList(specification);
        int count = await _repository.Count(new PriceClassificationSpecification(checkStatus:false));

        var result = AppUtils.ResultPaging<PriceClassificationPagingResult, PriceClassificationResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count: count,
            items: await AppUtils.MapObject<PriceClassification, PriceClassificationResponse>(priceClassifications,
                _mapper));
        return result;
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