using AutoMapper;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
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
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Specification;
using MailKit.Search;

namespace BusBookTicket.BillManage.Services.Bills;

public class BillService : IBillService
{
    private readonly IMapper _mapper;
    private readonly IBillItemService _billItemService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Bill> _repository;
    private readonly ITicketItemService _ticketItemService;
    private readonly IMailService _mailService;
    private readonly IGenericRepository<Customer> _customerRepo;
    private readonly IGenericRepository<Ticket_BusStop> _ticketBusStop;

    public BillService(
        IMapper mapper,
        ITicketItemService itemService,
        IBillItemService billItemService,
        IUnitOfWork unitOfWork,
        IMailService mailService
        )
    {
        _billItemService = billItemService;
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<Bill>();
        _mapper = mapper;
        _ticketItemService = itemService;
        _mailService = mailService;
        _customerRepo = unitOfWork.GenericRepository<Customer>();
        _ticketBusStop = unitOfWork.GenericRepository<Ticket_BusStop>();

    }
    public async Task<BillResponse> GetById(int id)
    {
        BillSpecification specification = new BillSpecification(id: id, checkStatus: false, getIsChangeStatus:false);
        Bill bill = await _repository.Get(specification);
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
            Bill bill = await _repository.Get(billSpecification);
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
            await SendMail(
                bill.Id,
                $"Đơn hàng {bill.Id} từ vé {bill.BillItems.ToList()[0].TicketItem.Ticket.Id} vừa bị hủy, ghế ngồi vừa được khôi phục",
                $"Ôii, mất đơn rồi!!",
                ""
            );
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

            Ticket_BusStop ticketBusStopEnd = await _ticketBusStop.Get(new TicketBusStopSpecification(entity.BusStationEndId, "Get"));
            Ticket_BusStop ticketBusStopStart= await _ticketBusStop.Get(new TicketBusStopSpecification(entity.BusStationStartId, "Get"));
            
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

                bill.TotalPrice += ticketItem.Price + ticketBusStopStart.DiscountPrice + ticketBusStopEnd.DiscountPrice;
                
            }

            // Update total price in bill
            bill.Status = (int)EnumsApp.AwaitingPayment;
            await _repository.Update(bill, userId);
            
            // Change status in Bill
            // await ChangeStatusToWaitingPayment(bill.Id, userId);
            Customer customer = await _customerRepo.Get(customerSpecification);
            //Send mail
            await SendMail(customer,
                $"Bạn vừa có một hóa đơn cho chuyến đi từ: {ticketBusStopStart.BusStop.BusStation!.Name} đến {ticketBusStopEnd.BusStop.BusStation!.Name}",
                "Hóa đơn của bạn",
                $"Giá: {bill.TotalPrice}"
            );
            await SendMail(ticketBusStopStart.Ticket.Bus.Company,
                $"Khách hàng: {customer.FullName} đã đặt hóa đơn cho chuyến đi từ: {ticketBusStopStart.BusStop.BusStation!.Name} đến {ticketBusStopEnd.BusStop.BusStation!.Name}",
                "Thần tài đến",
                $"Giá: {bill.TotalPrice}"
            );
            
            await _unitOfWork.SaveChangesAsync();
            
            _unitOfWork.Dispose();
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

    public Task<BillPagingResult> GetAll(BillPaging pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
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
        Bill bill = await _repository.Get(specification);
        bill.Customer = null;
        bill.BusStationEnd = null;
        bill.BusStationStart = null;

        return await _repository.ChangeStatus(bill, userId, (int)EnumsApp.AwaitingPayment);
    }

    public async Task<bool> ChangeStatusToPaymentComplete(int id, int userId)
    {
        BillSpecification specification = new BillSpecification(id, checkStatus:false, getIsChangeStatus:true);
        Bill bill = await _repository.Get(specification);

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
        Bill bill = await _repository.Get(specification);
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
        billSpecification.GetRevenue(companyId, year: year);
        List<Bill> bills = await _repository.ToList(billSpecification);
        var result = bills
            .Select(b => new
            {
                Revenue = b.TotalPrice,
                CompanyId = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Id : 0,
                CompanyName = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.Bus.Company.Name : "",
                Month = b.BillItems.Any() ? b.BillItems.First().TicketItem.Ticket.TicketBusStops.Any() ? b.BillItems.First().TicketItem.Ticket.TicketBusStops.First().DepartureTime.Month : 0 : 0
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
    
    public async Task<object> GetStatisticsStation(int year, int take, bool desc = true)
    {
        BillSpecification billSpecification = new BillSpecification();
        billSpecification.GetStatisticsStation(year);
        List<Bill> bills = await _repository.ToList(billSpecification);

        var resultArrival = bills
            .SelectMany(b => b.BillItems.Select(bi => new
            {
                StationStartId = b.BusStationStart?.BusStop?.BusStation?.Id,
                StationEndId = b.BusStationEnd?.BusStop?.BusStation?.Id,
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
                StationStartId = b.BusStationStart?.BusStop?.BusStation?.Id,
                StationEndId = b.BusStationEnd?.BusStop?.BusStation?.Id,
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
    #endregion -- Private Method --
}