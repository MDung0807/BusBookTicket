using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Ticket.Paging;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
    /// <param name="getAll"></param>
    public TicketSpecification(int id, bool getAll = false) : base(x => x.Id == id)
    {
        if(getAll) return;
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.Seats);
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.Bus.BusType);
        AddInclude(x => x.Bus.BusStops);
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
                Bus.BusNumber,
                Companies.Name
            FROM
                Ticket_BusStop t1
                INNER JOIN Tickets T ON T.Id = t1.TicketId
                LEFT JOIN TicketItems TItem ON TItem.TicketID = T.Id
                LEFT JOIN Buses Bus ON Bus.Id = T.BusID
                LEFT JOIN Companies ON Companies.Id = Bus.CompanyID
            WHERE
                t1.BusStopId IN (
                    SELECT BS.Id
                    FROM BusStations B
                        LEFT JOIN Wards W ON B.WardId = W.Id
                        LEFT JOIN Districts D ON W.DistrictId = D.Id
                        LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                        INNER JOIN BusStops BS ON BS.BusStationID = B.Id
                    WHERE
                        B.Name LIKE N'%' + @StationStart + N'%'
                        OR W.FullName LIKE N'%' + @StationStart + N'%'
                        OR D.FullName LIKE N'%' + @StationStart + N'%'
                        OR P.FullName LIKE N'%' + @StationStart + N'%'
                )
                AND t1.DepartureTime > @DateTime
                AND EXISTS (
                    SELECT 1
                    FROM Ticket_BusStop t2
                    WHERE
                        t2.TicketId = t1.TicketId
                        AND t2.BusStopId IN (
                            SELECT BS.Id
                            FROM BusStations B
                                LEFT JOIN Wards W ON B.WardId = W.Id
                                LEFT JOIN Districts D ON W.DistrictId = D.Id
                                LEFT JOIN Provinces P ON D.ProvinceId = P.Id
                                INNER JOIN BusStops BS ON BS.BusStationID = B.Id
                            WHERE
                                B.Name LIKE N'%' + @StationEnd + N'%'
                                OR W.FullName LIKE N'%' + @StationEnd + N'%'
                                OR D.FullName LIKE N'%' + @StationEnd + N'%'
                                OR P.FullName LIKE N'%' + @StationEnd + N'%'
                        )
                        AND t2.IndexStation > t1.IndexStation
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
                Bus.BusNumber,
                Companies.Name
        ");
        
        AddParameter("@StationStart", stationStart);
        AddParameter("@StationEnd", stationEnd);
        AddParameter("@DateTime", dateTime.ToString("yyyy-MM-dd"));
    }

    public TicketSpecification(int companyId, TicketPaging paging = null)
        : base(x => x.CreateBy == companyId)
    {
        if(paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.Seats);
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.Bus.BusType);
        AddInclude(x => x.Bus.BusStops); 
    }
}