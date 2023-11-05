using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.DiscountManager.Specification;

/// <summary>
/// Specification in Discount
/// </summary>
public class DiscountSpecification : BaseSpecification<Discount>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">is primary key in Discount</param>
    public DiscountSpecification(int id) : base(x => x.Id == id){}

    /// <summary>
    /// Constructor
    /// </summary>
    public DiscountSpecification(){}
}