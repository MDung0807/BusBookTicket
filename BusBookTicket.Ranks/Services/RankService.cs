﻿using AutoMapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Specification;

namespace BusBookTicket.Ranks.Services;

public class RankService : IRankService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Rank> _repository;

    public RankService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = _unitOfWork.GenericRepository<Rank>();
    }

    #region -- Public Method --
    public async Task<RankResponse>  GetById(int id)
    {
        RankSpecification rankSpecification = new RankSpecification(id);
        Rank rank = await _repository.Get(rankSpecification);
        return _mapper.Map<RankResponse>(rank);
    }

    public async Task<List<RankResponse>> GetAll()
    {
        RankSpecification rankSpecification = new RankSpecification();
        List<Rank> ranks = await _repository.ToList(rankSpecification);
        
        List<RankResponse> rankResponses = await AppUtils.MapObject<Rank,RankResponse>(ranks, _mapper);
        return rankResponses;
    }

    public async Task<bool> Update(RankUpdate entity, int id, int userId)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        rank.Id = id;
        await _repository.Update(rank, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        RankSpecification rankSpecification = new RankSpecification(id);
        Rank rank = await _repository.Get(rankSpecification);
        rank.Status = (int)EnumsApp.Delete;
        await _repository.Update(rank, userId);
        return true;
    }

    public async Task<bool>Create(RankCreate entity, int userId)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        await _repository.Create(rank, userId);
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

    public async Task<object> FindByParam(string param, object pagingRequest = default, bool checkStatus = true)
    {
        throw new NotImplementedException();
    }

    public Task<List<RankResponse>> GetAllByAdmin()
    {
        throw new NotImplementedException();
    }

    #endregion -- Public Method --
}