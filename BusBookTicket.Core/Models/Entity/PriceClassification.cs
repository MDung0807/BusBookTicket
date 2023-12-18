namespace BusBookTicket.Core.Models.Entity;

public class PriceClassification : BaseEntity
{
    public Company Company;
    public HashSet<StopStationDetail> StopStationDetails;
}