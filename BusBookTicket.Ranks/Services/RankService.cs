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
    public RankResponse getByID(int id)
    {
        Rank rank = _rankRepository.getByID(id);
        return _mapper.Map<RankResponse>(rank);
    }

    public List<RankResponse> getAll()
    {
        List<Rank> ranks = _rankRepository.getAll();
        List<RankResponse> rankResponses = new List<RankResponse>();
        
        rankResponses = AppUtils.MappObject<Rank,RankResponse>(ranks, _mapper);
        return rankResponses;
    }

    public bool update(RankUpdate entity, int id)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        rank.rankID = id;
        _rankRepository.update(rank);
        return true;
    }

    public bool delete(int id)
    {
        Rank rank = _rankRepository.getByID(id);
        rank.status = (int)EnumsApp.Delete;
        _rankRepository.delete(rank);
        return true;
    }

    public bool create(RankCreate entity)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        _rankRepository.create(rank);
        return true;
    }
    #endregion -- Public Method --
}