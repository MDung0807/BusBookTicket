namespace BusBookTicket.Buses.Repository;

public interface IBusTypeRepository
{
    Task<List<object>> TotalBusInType();
}