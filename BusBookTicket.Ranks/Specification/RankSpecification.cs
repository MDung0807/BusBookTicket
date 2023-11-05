using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Ranks.Specification;

public class RankSpecification : BaseSpecification<Rank>
{
    public RankSpecification(int id) : base(x => x.Id == id)
    { }
    public RankSpecification(){}
}