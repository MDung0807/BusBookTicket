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
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.Specification;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketItemServices;

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
    }
    public async Task<BillResponse> GetById(int id)
    {
        BillSpecification specification = new BillSpecification(id);
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

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
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
            bill.Customer = await _customerRepo.Get(customerSpecification);
            bill.Customer.Id = userId;
            bill = await _repository.Create(bill, userId);
            
            // Create item
            foreach (BillItemRequest item in itemsRequest)
            {
                item.BillId = bill.Id;
                BillItem billItem = await _billItemService.CreateBillItem(item, userId);
                
                //Cacl total Price
                TicketItemResponse ticketItem = await _ticketItemService.GetById(item.TicketItemId);
                
                // Change status in ticket item
                await _ticketItemService.ChangeStatusToWaitingPayment(item.TicketItemId, userId);

                bill.TotalPrice += ticketItem.Price;
            }

            // Update total price in bill
            bill.Status = (int)EnumsApp.AwaitingPayment;
            await _repository.Update(bill, userId);
            
            // Change status in Bill
            await ChangeStatusToWaitingPayment(bill.Id, userId);
            
            //Send mail
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToMail = bill.Customer.Email;
            mailRequest.Message = $"Bạn vừa có một hóa đơn cho chuyến đi từ: {bill.BusStationStart.Name} đến {bill.BusStationEnd.Name}";
            mailRequest.Content = $"Ghế của bạn là: {bill.BillItems}";
            mailRequest.Subject = "Hóa đơn của bạn";
            mailRequest.FullName = bill.Customer.FullName;
            await _mailService.SendEmailAsync(mailRequest);

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

    public Task<PagingResult<BillResponse>> GetAllByAdmin(PagingRequest pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatusToWaitingPayment(int id, int userId)
    {
        BillSpecification specification = new BillSpecification(id);
        Bill bill = await _repository.Get(specification);

        return await _repository.ChangeStatus(bill, userId, (int)EnumsApp.AwaitingPayment);
    }

    public async Task<bool> ChangeStatusToPaymentComplete(int id, int userId)
    {
        BillSpecification specification = new BillSpecification(id);
        Bill bill = await _repository.Get(specification);

        return await _repository.ChangeStatus(bill, userId, (int)EnumsApp.PaymentComplete);
    }

    public async Task<BillPagingResult> GetAllBillInUser(BillPaging paging, int userId)
    {
        BillSpecification billSpecification = new BillSpecification(userId, false, paging: paging);
        List<Bill> bills = await _repository.ToList(billSpecification);
        BillPagingResult result = new BillPagingResult();
        int count = await _repository.Count(new BillSpecification(userId:userId, checkStatus:false));
        result = AppUtils.ResultPaging<BillPagingResult, BillResponse>(
            paging.PageIndex,
            paging.PageSize,
            count,
            await AppUtils.MapObject<Bill, BillResponse>(bills, _mapper));
        return result;
    }
}