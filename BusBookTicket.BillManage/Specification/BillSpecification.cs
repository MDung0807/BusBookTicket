﻿using BusBookTicket.BillManage.Paging;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace BusBookTicket.BillManage.Specification;

public sealed class BillSpecification : BaseSpecification<Bill>
{

    public BillSpecification(int id = default, int userId = default, bool checkStatus = true, 
        BillPaging paging = null, int status = default, bool delete = false)
        : base(x => (userId == default || x.Customer.Id == userId) && 
                    (id == default|| x.Id == id ) &&
            ((delete == true && x.Status == status) || (status == default || x.Status == status) && delete == false ),
            checkStatus)
    {
        if (id != default)
        {
            // AddInclude(x => x.BillItems);
            return;
        }
        // AddInclude(x => x.BusStationEnd);
        // AddInclude(x => x.BusStationEnd.BusStop.BusStation);
        // AddInclude(x => x.BusStationStart.BusStop.BusStation);
        // AddInclude(x => x.BusStationStart);
        AddInclude(x => x.Customer);
        AddInclude(x => x.BillItems);
        AddInclude("BillItems.TicketItem.Ticket");
        AddInclude("BillItems.TicketItem.Ticket.TicketRouteDetails");
        
        if (paging != null)
            ApplyPaging(paging.PageIndex, paging.PageSize);
        ApplyOrderByDescending(x =>  x.DateUpdate);
    }

    public BillSpecification(int busId, int userId) : base(x =>
        x.Customer.Id == userId && x.BillItems.Any(p => p.TicketItem.Ticket.Bus.Id == busId))
    {
        
    }

    public BillSpecification(int id, bool getIsChangeStatus = false, bool checkStatus = true, DateTime dateTime = default)
        : base(x => x.Id == id  &&(dateTime == default 
                                   || x.TicketRouteDetailStart.DepartureTime >= dateTime.AddHours(2)),
            checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.BillItems);
            AddInclude("BillItems.TicketItem.Ticket");

            return;
        }
        AddInclude(x => x.BillItems);
        AddInclude("BillItems.TicketItem.Ticket");
        AddInclude("BillItems.TicketItem.Ticket.Bus");
        AddInclude("BillItems.TicketItem.Ticket.Bus.Company");
        AddInclude(x => x.TicketRouteDetailStart.RouteDetail.Station);
        AddInclude(x => x.TicketRouteDetailStart);
        AddInclude(x => x.TicketRouteDetailStart.RouteDetail);

    }

    public void Statistics(int year, int companyId = default)
    {
        Criteria = x => x.Status == (int)EnumsApp.Complete
                        && (companyId == default || x.BillItems.Any(bi => bi.TicketItem.Ticket.Bus.Company.Id == companyId))
                        && (year == default || x.BillItems.Any(bi => bi.TicketItem.Ticket.Date.Year == year));

        CheckStatus = false;
        AddInclude(b => b.BillItems);
        AddInclude("BillItems.TicketItem.Ticket.Bus.Company");
        // ApplyGroupBy(b => new { CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0, CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "", Month = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.TicketBusStops.Any() ? b.BillItems.First().TicketItem.Ticket.TicketBusStops.First().DepartureTime.Month:0 : 0});
    }
    
}