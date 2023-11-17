using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.CloudImage.Specification;

public class ImageSpecification : BaseSpecification<Images>
{
    public ImageSpecification(string objectModel, int key) : base(x=> x.ObjectModel == objectModel && x.Id01 == key)
    {
        
    }
}