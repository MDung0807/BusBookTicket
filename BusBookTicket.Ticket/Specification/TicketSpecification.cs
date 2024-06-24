using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Utils;
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
    public TicketSpecification(int id, bool getIsChangeStatus = false, bool checkStatus = true, int userId = default, bool isAsc = true) 
        : base(x => x.Id == id, checkStatus)
    {
        if (getIsChangeStatus)
        {
            Criteria = x => x.CreateBy == userId && x.Id == id;
            return;
        }
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.Seats);
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.Bus.BusType);
        AddInclude("TicketItems.BillItem.Bill.Customer");
    }

    public TicketSpecification(string stationStart, string stationEnd, DateTime dateTime,TicketPaging paging = null, List<int> companyIds = default,
        bool priceIsDesc = false, List<int> timeInDays = default)
    :base(x => (companyIds.Count == 0 || companyIds.Contains(x.Bus.Company.Id))
               && (timeInDays.Count == 0 || 
                   (timeInDays.Contains((int)EnumsApp.Morning) && x.Date.TimeOfDay.Hours >= 4 && x.Date.TimeOfDay.Hours <= 12) ||
                   (timeInDays.Contains((int)EnumsApp.Afternoon) && x.Date.TimeOfDay.Hours >= 12 && x.Date.TimeOfDay.Hours <= 17) ||
                   (timeInDays.Contains((int)EnumsApp.Evening) && x.Date.TimeOfDay.Hours >= 17 && x.Date.TimeOfDay.Hours <= 21) ||
                   (timeInDays.Contains((int)EnumsApp.Night) && (x.Date.TimeOfDay.Hours >= 21 || x.Date.TimeOfDay.Hours <= 4))
               )
               && x.Date > DateTime.Now)
    {
        if (paging!= null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }

        if (priceIsDesc)
        {
            ApplyOrderByDescending(x => x.TicketItems.FirstOrDefault().Price);
        }

        if (!priceIsDesc)
        {
            ApplyOrderBy(x => x.TicketItems.First().Price);
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
            x.TicketRouteDetails.Any(p => p.DepartureTime <= departureTime)
            && x.TicketRouteDetails.Any(p => p.ArrivalTime >= departureTime);
        // Criteria = x => x.Bus.Id == busId 
        //                 && x.TicketRouteDetails.First().DepartureTime <= departureTime
        //                 && x.TicketRouteDetails.Last().ArrivalTime >= departureTime;
        //
        // ApplyOrderBy(x => x.TicketRouteDetails.OrderBy(dt => dt.Id).Where(x => x.Ticket.Bus.Id == busId));
    }

    public  TicketSpecification(int companyId, bool checkStatus = true, TicketPaging paging = null, DateOnly month = default)
        : base(x => x.Bus.Company.Id == companyId
            && (month == default || x.Date.Month == month.Month && x.Date.Year == month.Year), checkStatus: false)
    {
        if (paging != null)
        {
            ApplyOrderByDescending(x => x.Date);
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
    
    public TicketSpecification(){}

    public void DepartureBeforeMinute(int minute, bool checkStatus = false)
    {
        Criteria = x => (x.Date - DateTime.Now).Minutes == minute 
                        && x.TicketRouteDetails.FirstOrDefault().ArrivalTime == null
                        && x.Status != (int)EnumsApp.Delete;
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.TicketRouteDetails);
    }

    public void CompleteTicket()
    {
        Criteria = x => (x.TicketRouteDetails.Last().ArrivalTime).Value.Minute == 0
                        && !(x.TicketRouteDetails.Last().DepartureTime == null);
    }

    public void GetTickets(List<int> ticketIds, bool checkStatus)
    {
        Criteria = x => ticketIds.Contains(x.Id)
                        && x.Status != (int)EnumsApp.Delete;
    }
}