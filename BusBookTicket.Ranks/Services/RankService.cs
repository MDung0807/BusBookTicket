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
        return _rankRepository.update(rank);
    }

    public bool delete(int id)
    {
        Rank rank = _rankRepository.getByID(id);
        rank.status = (int)EnumsApp.Delete;
        return _rankRepository.delete(rank);
    }

    public bool create(RankCreate entity)
    {
        Rank rank = _mapper.Map<Rank>(entity);
        return _rankRepository.create(rank);
    }
    #endregion -- Public Method --
}