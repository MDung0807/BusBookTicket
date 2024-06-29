using System.Globalization;
using System.Reflection;
using AutoMapper;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.BillManage.DTOs.Requests;
using BusBookTicket.BillManage.DTOs.Responses;
using BusBookTicket.BillManage.Paging;
using BusBookTicket.BillManage.Services.BillItems;
using BusBookTicket.BillManage.Specification;
using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Application.Paging;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.Specification;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Service;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Specification;
using MailKit.Search;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
namespace BusBookTicket.BillManage.Services.Bills;
using PayPal.Api;

public class BillService : IBillService
{
    private readonly IMapper _mapper;
    private readonly IBillItemService _billItemService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Bill> _repository;
    private readonly ITicketItemService _ticketItemService;
    private readonly IMailService _mailService;
    private readonly IGenericRepository<Customer> _customerRepo;
    private readonly IGenericRepository<Ticket_RouteDetail> _ticketRouteDetail;
    private readonly IConfiguration _configuration;
    private bool _billIsPaymented = false;

    private readonly IRouteDetailService _routeDetailService;
    private readonly IMemoryCache _cache;
    private readonly INotificationService _notificationService;

    public BillService(
        IMapper mapper,
        ITicketItemService itemService,
        IBillItemService billItemService,
        IUnitOfWork unitOfWork,
        IMailService mailService,
        IRouteDetailService routeDetailService, IMemoryCache cache, IConfiguration configuration, INotificationService notificationService)
    {
        _billItemService = billItemService;
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<Bill>();
        _mapper = mapper;
        _ticketItemService = itemService;
        _mailService = mailService;
        _customerRepo = unitOfWork.GenericRepository<Customer>();
        _routeDetailService = routeDetailService;
        _cache = cache;
        _configuration = configuration;
        _notificationService = notificationService;
        _ticketRouteDetail = unitOfWork.GenericRepository<Ticket_RouteDetail>();

    }
    public async Task<BillResponse> GetById(int id)
    {
        BillSpecification specification = new BillSpecification(id: id, checkStatus: false, getIsChangeStatus:false);
        Bill bill = await _repository.Get(specification, checkStatus: false);
        BillResponse response = _mapper.Map<BillResponse>(bill);
        response.Items = await _billItemService.GetItemInBill(bill.Id);
        return response;
    }

    public Task<List<BillResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(BillRequest entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    [Obsolete("Obsolete")]
    public async Task<bool> Delete(int id, int userId)
    {
        if (! await ChangeBillCanDelete(id))
        {
            throw new ExceptionDetail(BillConstants.DELETE_ERROR);
        }
        try
        {
            await _unitOfWork.BeginTransaction();
            BillSpecification billSpecification =
                new BillSpecification(id, checkStatus: false, getIsChangeStatus: true);
            Bill bill = await _repository.Get(billSpecification, checkStatus: false);

            if (!PayPalRefund(bill.PaypalTransactionId, bill.TotalPrice)) 
                return false;
            foreach (var item in bill.BillItems)
            {
                await _ticketItemService.ChangeIsActive(item.Id, userId);
            }
            List<Dictionary<string, int>> listObjectNotChange = new List<Dictionary<string, int>>
            {
                new Dictionary<string, int>
                {
                    {"TicketItem", 0} // You can set the integer value accordingly
                }
            };
            await _repository.ChangeStatus(bill, userId: userId, (int)EnumsApp.Delete, listObjectNotChange);
            // await SendMail(
            //     bill.Id,
            //     $"Đơn hàng {bill.Id} từ vé {bill.BillItems.ToList()[0].TicketItem.Ticket.Id} vừa bị hủy, ghế ngồi vừa được khôi phục",
            //     $"Ôii, mất đơn rồi!!",
            //     ""
            // );
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            await _unitOfWork.RollbackTransactionAsync();
            throw new ExceptionDetail(BillConstants.ERROR);
        }
    }

    public async Task<bool> Create(BillRequest entity, int userId)
    {
        List<BillItemRequest> itemsRequest = entity.ItemsRequest;
        await _unitOfWork.BeginTransaction();
        try
        {
            // Create bill
            Bill bill = _mapper.Map<Bill>(entity);
            CustomerSpecification customerSpecification = new CustomerSpecification(userId);
            Ticket_RouteDetail ticketRouteDetailStart = await _ticketRouteDetail.Get(new TicketRouteDetailSpec(id: entity.TicketRouteDetailStartId), checkStatus: false);
            Ticket_RouteDetail ticketRouteDetailEnd = await _ticketRouteDetail.Get(new TicketRouteDetailSpec(id: entity.TicketRouteDetailEndId), checkStatus: false);

            // Ticket_BusStop ticketBusStopEnd = await _ticketBusStop.Get(new TicketBusStopSpecification(entity.BusStationEndId, "Get"));
            // Ticket_BusStop ticketBusStopStart= await _ticketBusStop.Get(new TicketBusStopSpecification(entity.BusStationStartId, "Get"));
            bill.Customer = new Customer();
            bill.Customer.Id = userId;
            bill = await _repository.Create(bill, userId);

            bool isFirst = true;
            // Create item
            foreach (BillItemRequest item in itemsRequest)
            {
                item.BillId = bill.Id;
                BillItem billItem = await _billItemService.CreateBillItem(item, userId);
                //Cacl total Price
                TicketItemResponse ticketItem = await _ticketItemService.GetById(item.TicketItemId);
                // Change status in ticket item
                await _ticketItemService.ChangeStatusToWaitingPayment(item.TicketItemId, userId);

                bill.TotalPrice += ticketItem.Price + (int)ticketRouteDetailStart.RouteDetail.DiscountPrice + (int)ticketRouteDetailEnd.RouteDetail.DiscountPrice;
                
            }

            // Update total price in bill
            if (_billIsPaymented)
                bill.Status = (int)EnumsApp.PaymentComplete;
            else
                bill.Status = (int)EnumsApp.AwaitingPayment;
            await _repository.Update(bill, userId);
            
            // Change status in Bill
            // await ChangeStatusToWaitingPayment(bill.Id, userId);
            Customer customer = await _customerRepo.Get(customerSpecification);
            //Send mail
            await SendMail(customer,
                $"Bạn vừa có một hóa đơn cho chuyến đi từ: {ticketRouteDetailStart.RouteDetail.Station.Name} đến {ticketRouteDetailEnd.RouteDetail.Station.Name}",
                "Hóa đơn của bạn",
                $"Giá: {bill.TotalPrice}"
            );
            await SendMail(ticketRouteDetailStart.RouteDetail.Company,
                $"Khách hàng: {customer.FullName} đã đặt hóa đơn cho chuyến đi từ: {ticketRouteDetailStart.RouteDetail.Station.Name} đến {ticketRouteDetailEnd.RouteDetail.Station.Name}",
                "Thần tài đến",
                $"Giá: {bill.TotalPrice}"
            );
            
            await _unitOfWork.SaveChangesAsync();
            string content = $"{customer.FullName} đã đặt vé xe ";
            await SendNotification(content, $"{AppConstants.COMPANY}_{ticketRouteDetailEnd.RouteDetail.Company.Id}",
                $"${customer.FullName}", "", customer.Id);
            _unitOfWork.Dispose();
            RemoveItemInCache(entity);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            _unitOfWork.Dispose();
            throw new Exception(BillConstants.ERROR);
        }

        return true;
    }

    public Task<bool> ChangeIsActive(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsLock(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeToWaiting(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeToWaiting(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatus(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeToDisable(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckToExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    public Task<BillPagingResult> GetAllByAdmin(BillPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<BillPagingResult> GetAll(BillPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<BillPagingResult> GetAll(BillPaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BillPagingResult> FindByParam(string param, BillPaging pagingRequest = default, bool checkStatus = true)
    {
        throw new NotImplementedException();
    }

    public Task<PagingResult<BillResponse>> GetAllByAdmin(PagingRequest pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatusToWaitingPayment(int id, int userId)
    {
        BillSpecification specification = new BillSpecification(id, checkStatus:false, getIsChangeStatus:true);
        Bill bill = await _repository.Get(specification, checkStatus: false);
        bill.Customer = null;
        return await _repository.ChangeStatus(bill, userId, (int)EnumsApp.AwaitingPayment);
    }

    public async Task<bool> ChangeStatusToPaymentComplete(int id, int userId)
    {
        BillSpecification specification = new BillSpecification(id, checkStatus:false, getIsChangeStatus:true);
        Bill bill = await _repository.Get(specification, checkStatus: false);

        return await _repository.ChangeStatus(bill, userId, (int)EnumsApp.PaymentComplete);
    }

    public async Task<BillPagingResult> GetAllBillInUser(BillPaging paging, int userId)
    {
        BillSpecification billSpecification = new BillSpecification(userId:userId, checkStatus:false, paging: paging);
        List<Bill> bills = await _repository.ToList(billSpecification);
        BillPagingResult result = new BillPagingResult();
        int count = await _repository.Count(new BillSpecification(userId:userId, checkStatus:false));

        List<BillResponse> billResponses = await AppUtils.MapObject<Bill, BillResponse>(bills, _mapper);

        foreach (var item in billResponses)
        {
            item.Items = await _billItemService.GetItemInBill(item.Id);
        }
        result = AppUtils.ResultPaging<BillPagingResult, BillResponse>(
            paging.PageIndex,
            paging.PageSize,
            count,
            items: billResponses
            );
        return result;
    }

    public async Task<bool> ChangeCompleteStatus(int billId, int userId)
    {
        BillSpecification specification = new BillSpecification(billId, checkStatus:false, getIsChangeStatus: true);
        Bill bill = await _repository.Get(specification, checkStatus: false);
        await _repository.ChangeStatus(bill, userId: userId, (int)EnumsApp.Active);
        return true;
    }

    public async Task<BillResponse> GetBillByUserAndBus(int userId, int busId)
    {
        BillSpecification specification = new BillSpecification(userId: userId, busId: busId);
        Bill bill =await _repository.Get(specification);
        BillResponse response = _mapper.Map<BillResponse>(bill);
        return response;
    }

    public async Task<BillPagingResult> GetAllInWaitingStatus(BillPaging paging, int userId)
    {
        BillSpecification billSpecification = new BillSpecification(userId:userId, checkStatus:false, paging: paging, status: (int)EnumsApp.AwaitingPayment);
        List<Bill> bills = await _repository.ToList(billSpecification);
        BillPagingResult result = new BillPagingResult();
        int count = await _repository.Count(new BillSpecification(userId:userId, checkStatus:false));

        List<BillResponse> billResponses = await AppUtils.MapObject<Bill, BillResponse>(bills, _mapper);

        foreach (var item in billResponses)
        {
            item.Items = await _billItemService.GetItemInBill(item.Id);
        }
        result = AppUtils.ResultPaging<BillPagingResult, BillResponse>(
            paging.PageIndex,
            paging.PageSize,
            count,
            items: billResponses
        );
        return result;
    }

    public async Task<BillPagingResult> GetAllInDeleteStatus(BillPaging paging, int userId)
    {
        BillSpecification billSpecification = new BillSpecification(userId:userId, checkStatus:false, paging: paging, status: (int)EnumsApp.Delete, delete:true);
        List<Bill> bills = await _repository.ToList(billSpecification);
        BillPagingResult result = new BillPagingResult();
        int count = await _repository.Count(new BillSpecification(userId:userId, checkStatus:false));

        List<BillResponse> billResponses = await AppUtils.MapObject<Bill, BillResponse>(bills, _mapper);

        foreach (var item in billResponses)
        {
            item.Items = await _billItemService.GetItemInBill(item.Id);
        }
        result = AppUtils.ResultPaging<BillPagingResult, BillResponse>(
            paging.PageIndex,
            paging.PageSize,
            count,
            items: billResponses
        );
        return result;
    }

    public async Task<BillPagingResult> GetAllInCompleteStatus(BillPaging paging, int userId)
    {
        BillSpecification billSpecification = new BillSpecification(userId:userId, checkStatus:false, paging: paging, status: (int)EnumsApp.Complete);
        List<Bill> bills = await _repository.ToList(billSpecification);
        BillPagingResult result = new BillPagingResult();
        int count = await _repository.Count(new BillSpecification(userId:userId, checkStatus:false));

        List<BillResponse> billResponses = await AppUtils.MapObject<Bill, BillResponse>(bills, _mapper);

        foreach (var item in billResponses)
        {
            item.Items = await _billItemService.GetItemInBill(item.Id);
        }
        result = AppUtils.ResultPaging<BillPagingResult, BillResponse>(
            paging.PageIndex,
            paging.PageSize,
            count,
            items: billResponses
        );
        return result;
    }

    public async Task<object> RevenueStatistics(int companyId, int year)
    {
        BillSpecification billSpecification = new BillSpecification();
        billSpecification.Statistics(year: year, companyId);
        List<Bill> bills = await _repository.ToList(billSpecification);
        var result = bills
            .Select(b => new
            {
                Revenue = b.TotalPrice,
                CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0,
                CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "",
                Month = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Date.Month : 0
            })
            .GroupBy(x => new { x.CompanyId, x.CompanyName, x.Month})
            .Select(group => new
            {
                CompanyId = group.Key.CompanyId,
                CompanyName = group.Key.CompanyName,
                Month = group.Key.Month,
                TotalRevenue = group.Sum(b => b.Revenue) // Adjust this part based on your actual property
            })
            .ToList();
        return result;
    }
    
    public async Task<object> RevenueStatisticsByQuarter(int companyId, int year)
    {
        BillSpecification billSpecification = new BillSpecification();
        billSpecification.Statistics(year: year, companyId);
        List<Bill> bills = await _repository.ToList(billSpecification);
        var result = bills
            .Select(b => new
            {
                Revenue = b.TotalPrice,
                CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0,
                CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "",
                Quarter = GetQuarter(b, true)
            })
            .GroupBy(x => new { x.CompanyId, x.CompanyName, x.Quarter})
            .Select(group => new
            {
                CompanyId = group.Key.CompanyId,
                CompanyName = group.Key.CompanyName,
                Month = group.Key.Quarter,
                TotalRevenue = group.Sum(b => b.Revenue) // Adjust this part based on your actual property
            })
            .ToList();
        return result;
    }
    
    public async Task<object> GetStatisticsStationByAdmin(int year, int take, bool desc = true)
    {
        BillSpecification billSpecification = new BillSpecification();
        billSpecification.Statistics(year);
        List<Bill> bills = await _repository.ToList(billSpecification);

        var resultArrival = bills
            .SelectMany(b => b.BillItems.Select(bi => new
            {
                StationStartId = b.TicketRouteDetailStart.RouteDetail.Station.Id,
                StationEndId = b.TicketRouteDetailEnd.RouteDetail.Station.Id,
                PassengerCountArrival = b.BillItems.Count
            }))
            .Where(x => x.StationStartId != null)
            .GroupBy(x => x.StationStartId)
            .Select(group => new
            {
                StationStartId = group.Key,
                TotalPassengerCountArrival = group.Sum(x => x.PassengerCountArrival)
            })
            .ToList();

        var resultDeparture = bills
            .SelectMany(b => b.BillItems.Select(bi => new
            {
                StationStartId = b.TicketRouteDetailStart.RouteDetail.Station.Id,
                StationEndId = b.TicketRouteDetailEnd.RouteDetail.Station.Id,
                PassengerCountDeparture = b.BillItems.Count
            }))
            .Where(x => x.StationEndId != null)
            .GroupBy(x => x.StationEndId)
            .Select(group => new
            {
                StationEndId = group.Key,
                TotalPassengerCountDeparture = group.Sum(x => x.PassengerCountDeparture)
            })
            .ToList();

        if (desc)
        {
            resultArrival = resultArrival.OrderByDescending(x => x.TotalPassengerCountArrival).Take(take).ToList();
            resultDeparture = resultDeparture.OrderByDescending(x => x.TotalPassengerCountDeparture).Take(take).ToList();
        }
        else
        {
            resultArrival = resultArrival.OrderBy(x => x.TotalPassengerCountArrival).Take(take).ToList();
            resultDeparture = resultDeparture.OrderBy(x => x.TotalPassengerCountDeparture).Take(take).ToList();
        }

        return new { Arrival = resultArrival, Departure = resultDeparture };
    }

    public async Task<object> GetStatisticsStationByCompany(int companyId, int year, int take, bool desc = true)
    {
        BillSpecification billSpecification = new BillSpecification();
        billSpecification.Statistics(year, companyId);
        List<Bill> bills = await _repository.ToList(billSpecification);

        var resultArrival = bills
            .SelectMany(b => b.BillItems.Select(bi => new
            {
                StationStartId = b.TicketRouteDetailStart.RouteDetail.Station.Id,
                StationEndId = b.TicketRouteDetailEnd.RouteDetail.Station.Id,
                CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0,
                CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "",
                PassengerCountArrival = b.BillItems.Count
            }))
            .Where(x => x.StationStartId != null)
            .GroupBy(x => new
            {
                x.StationStartId,
                x.CompanyName,    
                x.CompanyId
            })
            .Select(group => new
            {
                StationStartId = group.Key.StationStartId,
                CompanyId = group.Key.CompanyId,
                CompanyName = group.Key.CompanyName,
                TotalPassengerCountArrival = group.Sum(x => x.PassengerCountArrival)
            })
            .ToList();

        var resultDeparture = bills
            .SelectMany(b => b.BillItems.Select(bi => new
            {
                StationStartId = b.TicketRouteDetailStart.RouteDetail.Station.Id,
                StationEndId = b.TicketRouteDetailEnd.RouteDetail.Station.Id,
                CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0,
                CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "",
                PassengerCountDeparture = b.BillItems.Count
            }))
            .Where(x => x.StationEndId != null)
            .GroupBy(x => new
            {
                x.StationEndId,
                x.CompanyName,
                x.CompanyId
            })
            .Select(group => new
            {
                StationEndId = group.Key.StationEndId,
                CompanyId = group.Key.CompanyId,
                CompanyName = group.Key.CompanyName,
                TotalPassengerCountDeparture = group.Sum(x => x.PassengerCountDeparture)
            })
            .ToList();

        if (desc)
        {
            resultArrival = resultArrival.OrderByDescending(x => x.TotalPassengerCountArrival).Take(take).ToList();
            resultDeparture = resultDeparture.OrderByDescending(x => x.TotalPassengerCountDeparture).Take(take).ToList();
        }
        else
        {
            resultArrival = resultArrival.OrderBy(x => x.TotalPassengerCountArrival).Take(take).ToList();
            resultDeparture = resultDeparture.OrderBy(x => x.TotalPassengerCountDeparture).Take(take).ToList();
        }

        return new { Arrival = resultArrival, Departure = resultDeparture };
    }

    public Task Reserve(BillRequest request, int userId)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
            .SetPriority(CacheItemPriority.Normal);

        foreach (var item in request.ItemsRequest)
        {
            _cache.Set($"{item.TicketItemId}", request, cacheEntryOptions);
        }
        return Task.CompletedTask;
    }

    public Task<bool> CheckReserve(BillRequest request, int userId)
    {
        foreach (var item in request.ItemsRequest)
        {
            if (_cache.Get($"{item.TicketItemId}") == null)
            {
                return Task.FromResult(false);
            }
        }
        return Task.FromResult(true);
    }

    public async Task<bool> PaymentPaypal(BillRequest request, int userId)
    {
        if (request.PaypalTransactionId != null)
        {
            _billIsPaymented = true;
        }
        return await Create(request, userId);
    }

    #region  -- Private Method --

    private async Task<bool> ChangeBillCanDelete(int id)
    {
        BillSpecification specification = new BillSpecification(id, getIsChangeStatus: false, checkStatus:false, dateTime:DateTime.Now);
        return await _repository.Contains(specification);
    }

    private async Task<bool> SendMail(Object obj, string message, string subject, string content)
    {
        MailRequest mailRequest = new MailRequest();
        mailRequest.ToMail = (string)obj.GetType().GetProperty("Email")?.GetValue(obj);
        mailRequest.Message = message;
        mailRequest.Content = content;
        mailRequest.Subject = subject;
        mailRequest.FullName = (string)(obj.GetType().GetProperty("FullName")?.GetValue(obj) == null
            ? obj.GetType().GetProperty("Name")?.GetValue(obj)
            : obj.GetType().GetProperty("FullName")?.GetValue(obj));
        await _mailService.SendEmailAsync(mailRequest);
        return true;
    }

    private async Task<bool> SendMail(int billId, string message, string subject, string content)
    {
        BillSpecification specification = new BillSpecification(billId, false, checkStatus:false);
        Bill bill = await _repository.Get(specification);
        Company company = bill.BillItems.ToList()[0].TicketItem.Ticket.Bus.Company;
        MailRequest mailRequest = new MailRequest();
        mailRequest.ToMail = company.Email;
        mailRequest.Message = message;
        mailRequest.Content = content;
        mailRequest.Subject = subject;
        mailRequest.FullName = company.Name;
        await _mailService.SendEmailAsync(mailRequest);
        return true;
    }

    private int GetQuarter(Bill b, bool isQuarter = false)
    {
        int month =  b.BillItems.Any()
            ? b.BillItems.First().TicketItem.Ticket.TicketRouteDetails.Any()
                ? b.BillItems.First().TicketItem.Ticket.Date.Month
                : 0
            : 0;
        if (!isQuarter)
            return month;
        if (month >= 1 && month <= 3)
        {
            return 1; // Quý 1: Tháng 1, 2, 3
        }
        else if (month >= 4 && month <= 6)
        {
            return 2; // Quý 2: Tháng 4, 5, 6
        }
        else if (month >= 7 && month <= 9)
        {
            return 3; // Quý 3: Tháng 7, 8, 9
        }
        else
        {
            return 4; // Quý 4: Tháng 10, 11, 12
        }
    }

    [Obsolete("Obsolete")]
    private bool PayPalRefund(string transactionId, long totalPrice)
    {
        string clientId = _configuration["PayPalOptions:ClientId"];
        string clientSecret = _configuration["PayPalOptions:ClientSecret"];
        string mode = _configuration["PayPalOptions:Mode"];
        var config = new Dictionary<string, string>
        {
            {"mode", mode},
            {"clientId", clientId},
            {"clientSecret", clientSecret}
        };
        try
        {
            var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            // Retrieve the sale transaction by ID
            var sale = new Sale
            {
                id = transactionId
            };

            // Create a refund object
            var refund = new Refund
            {
                amount = new Amount
                {
                    currency = "USD",
                    total = Math.Ceiling((decimal)totalPrice/24000).ToString(CultureInfo.InvariantCulture)// Amount to be refunded
                }
            };

            // Refund the sale
            var response = sale.Refund(apiContext, refund);

            // Check the refund status
            if (response.state == "completed")
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Refund failed: {response.state}");
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(AppConstants.ERROR_IN_PAYPAL_REFUND);
        }
    }

    private void RemoveItemInCache(BillRequest request)
    {
        foreach (var item in request.ItemsRequest)
        {
            _cache.Remove($"{item.TicketItemId}");
        }
    }

    private async Task SendNotification(string content, string actor, string sender, string href, int userId)
    {
        AddNewNotification newNotification = new AddNewNotification
        {
            Content = content,
            Actor = actor,
            Href = href,
            Sender = sender
        };
        await _notificationService.InsertNotification(newNotification, userId);
    }
    #endregion -- Private Method --
}