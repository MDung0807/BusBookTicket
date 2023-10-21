using AutoMapper;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;
using BusBookTicket.Ranks.DTOs.Requests;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Repositories;

namespace BusBookTicket.Ranks.Services;

public class RankService : IRankService
{
    private readonly IRankRepository _rankRepository;
    private readonly IMapper _mapper;

    public RankService(IRankRepository repository, IMapper mapper)
    {
        this._mapper = mapper;
        this._rankRepository = repository;
    }

    #region -- Public Method --
    public async Task<RankResponse>  getByID(int id)
    {
        Rank rank = await _rankRepository.getByID(id);
        return _mapper.Map<RankResponse>(rank);
    }

    public async Task<List<RankResponse>> getAll()
    {
        List<Rank> ranks = await _rankRepository.getAll();

        List<RankResponse> rankResponses = await AppUtils.MappObject<Rank,RankResponse>(ranks, _mapper);
        return rankResponses;
    }

    public async Task<bool> update(RankUpdate entity, int id)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        rank.rankID = id;
        await _rankRepository.update(rank);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Rank rank = await _rankRepository.getByID(id);
        rank.status = (int)EnumsApp.Delete;
        await _rankRepository.delete(rank);
        return true;
    }

    public async Task<bool>create(RankCreate entity)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        await _rankRepository.create(rank);
        return true;
    }
    #endregion -- Public Method --
}