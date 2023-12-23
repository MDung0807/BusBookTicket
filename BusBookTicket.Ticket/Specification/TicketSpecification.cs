using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Ticket.Paging;

namespace BusBookTicket.Ticket.Specification;

/// <summary>
/// Specification in Ticket
/// </summary>
public sealed class TicketSpecification : BaseSpecification<Core.Models.Entity.Ticket>
{
    /// <summary>
    /// Find Ticket By Id
    /// </summary>
    /// <param name="id">Primary key in Ticket</param>
    /// <param name="getIsChangeStatus"></param>
    /// <param name="checkStatus"></param>
    /// <param name="userId"></param>
    public TicketSpecification(int id, bool getIsChangeStatus = false, bool checkStatus = true, int userId = default) : base(x => x.Id == id, checkStatus)
    {
        if (getIsChangeStatus)
        {
            Criteria = x => x.CreateBy == userId;
            return;
        }
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.Seats);
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.Bus.BusType);
        AddInclude("TicketItems.BillItem.Bill.Customer");
    }

    public TicketSpecification(string stationStart, string stationEnd, DateTime dateTime,TicketPaging paging = null)
    {
        if (paging!= null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddSqlQuery(@"
            SELECT
                    T.Id,
                    T.Date,
                    T.BusID,
                    T.DateCreate,
                    T.DateUpdate,
                    T.UpdateBy,
                    T.CreateBy,
                    T.Status,
                    T.PriceClassificationId,
					T.StopStationId,
                    Bus.BusNumber,
                    Companies.Name
                FROM
                    TicketRouteDetails t1
                    INNER JOIN Tickets T ON T.Id = t1.TicketId
                    LEFT JOIN TicketItems TItem ON TItem.TicketID = T.Id
                    LEFT JOIN Buses Bus ON Bus.Id = T.BusID
                    LEFT JOIN Companies ON Companies.Id = Bus.CompanyID
					INNER JOIN RouteDetails RD1 ON RD1.ID = t1.RouteDetailId

                WHERE
                    t1.RouteDetailId IN (
                        SELECT BS1.Id
                        FROM BusStations B
                            LEFT JOIN Wards W ON B.WardId = W.Id
                            LEFT JOIN Districts D ON W.DistrictId = D.Id
                            LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                            INNER JOIN RouteDetails BS1 ON BS1.StationId = B.Id
                        WHERE
                            B.Name LIKE N'%' + @StationStart + N'%'
                            OR W.FullName LIKE N'%' + @StationStart + N'%'
                            OR D.FullName LIKE N'%' + @StationStart + N'%'
                            OR P.FullName LIKE N'%' + @StationStart + N'%'
                    )
                    AND DATEDIFF(day,t1.DepartureTime, @DateTime) = 0
                    AND EXISTS (
                        SELECT 1
                        FROM TicketRouteDetails t2
						INNER JOIN RouteDetails RD2 ON RD2.ID = t2.RouteDetailId
                        WHERE
                            t2.TicketId = t1.TicketId
                            AND t2.RouteDetailId IN (
                                SELECT BS2.Id
                                FROM BusStations B
                                    LEFT JOIN Wards W ON B.WardId = W.Id
                                    LEFT JOIN Districts D ON W.DistrictId = D.Id
                                    LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                                    INNER JOIN RouteDetails BS2 ON BS2.StationId = B.Id
                                WHERE
                                    B.Name LIKE N'%' + @StationEnd + N'%'
                                    OR W.FullName LIKE N'%' + @StationEnd + N'%'
                                    OR D.FullName LIKE N'%' + @StationEnd + N'%'
                                    OR P.FullName LIKE N'%' + @StationEnd + N'%'
                            )
                            AND RD2.IndexStation > RD1.IndexStation
                        AND T.Status = 1
                    )
                GROUP BY
                    T.Id,
                    T.Date,
                    T.BusID,
                    T.DateCreate,
                    T.DateUpdate,
                    T.UpdateBy,
                    T.CreateBy,
                    T.Status,
                    T.PriceClassificationId,
					T.StopStationId,
                    Bus.BusNumber,
                    Companies.Name

        ");
        
        AddParameter("@StationStart", stationStart);
        AddParameter("@StationEnd", stationEnd);
        AddParameter("@DateTime", dateTime.ToString("yyyy-MM-dd"));
    }

    public TicketSpecification(int busId, DateTime departureTime= default)
    {
        Criteria = x => x.Bus.Id == busId &&
            x.TicketRouteDetails.Any(p => p.DepartureTime <= departureTime &&
                                      p.ArrivalTime>= departureTime);
        
        AddInclude(x => x.TicketBusStops);
    }

    public  TicketSpecification(int companyId, bool checkStatus = true, TicketPaging paging = null)
        : base(x => x.Bus.Company.Id == companyId, checkStatus: false)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}