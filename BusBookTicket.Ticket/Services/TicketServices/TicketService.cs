
using AutoMapper;
using BusBookTicket.AddressManagement.Services.WardService;
using BusBookTicket.AddressManagement.Utilities;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.DTOs.Responses;
using BusBookTicket.PriceManage.Services;
using BusBookTicket.RoutesManage.DTOs.Responses;
using BusBookTicket.RoutesManage.Service;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Paging;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Specification;
using BusBookTicket.Ticket.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace BusBookTicket.Ticket.Services.TicketServices;

public class TicketService : ITicketService
{
    #region -- Properties --

    private readonly ITicketItemService _itemService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Core.Models.Entity.Ticket> _repository;
    private readonly IGenericRepository<Bus> _busRepository;
    private readonly IGenericRepository<TicketItem> _ticketItemRepository;
    private readonly IImageService _imageService;
    private readonly IWardService _wardService;
    private readonly IMailService _mailService;
    private readonly IGenericRepository<Ticket_RouteDetail> _ticketRouteDetail;
    private readonly IRouteDetailService _routeDetail;
    private readonly IPriceService _priceService;
    private readonly IPriceClassificationService _priceClassification;
    private readonly IMemoryCache _cache;

    #endregion -- Properties --

    public TicketService(
        ITicketItemService itemService, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IImageService imageService,
        IWardService wardService,
        IMailService mailService,
        IRouteDetailService routeDetailService,
        IPriceService priceService,
        IPriceClassificationService priceClassificationService, IMemoryCache cache)
    {
        this._itemService = itemService;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
        _busRepository = unitOfWork.GenericRepository<Bus>();
        _ticketItemRepository = unitOfWork.GenericRepository<TicketItem>();
        _imageService = imageService;
        _wardService = wardService;
        _mailService = mailService;
        _ticketRouteDetail = unitOfWork.GenericRepository<Ticket_RouteDetail>();
        _routeDetail = routeDetailService;
        _priceClassification = priceClassificationService;
        _cache = cache;
        _priceService = priceService;
    }
    public TicketService(){}
    
    public async Task<TicketResponse> GetById(int id)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(id,checkStatus:true, getIsChangeStatus:false);
        Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
        if (ticket == null)
        {
            return null;
        }
        List<TicketItemResponse> itemResponses = await _itemService.GetAllInTicket(id);
        int totalEmptySeat = 0;
        foreach (var item in itemResponses)
        {
            if (_cache.Get($"{item.Id}") != null)
                item.Status = (int)EnumsApp.AwaitingPayment;
            if (item.Status == 1)
            {
                totalEmptySeat++;
            }
        }
        TicketResponse response = _mapper.Map<TicketResponse>(ticket);
        response.TotalEmptySeat = totalEmptySeat;
        List<string> images = await _imageService.getImages(typeof(Company).ToString(), id);
        response.CompanyLogo = images.Count > 0 ? images[0] : null;
        response.ItemResponses = new List<TicketItemResponse>();
        response.ItemResponses = itemResponses;
        response.ListStation = new List<StationResponse>();
        response.ListStation= await GetAllBusStopInTicket(id);
        return response;
    }

    public Task<List<TicketResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(TicketFormUpdate entity, int id, int userId)
    {
        Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
        await _repository.Update(ticket, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();
            TicketSpecification ticketSpecification = new TicketSpecification(id: id, checkStatus: false, getIsChangeStatus:true, userId:userId);
            Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification, checkStatus: false);
            TicketItemSpecification ticketItemSpecification =
                new TicketItemSpecification(0, id, true, checkStatus: false);
            List<TicketItem> items = await _ticketItemRepository.ToList(ticketItemSpecification);
            ticket.TicketItems = new HashSet<TicketItem>(items);
            await _repository.ChangeStatus(ticket, userId, (int)EnumsApp.Delete);

            // await SendMails(
            //     ticket,
            //     $"Vé đã bị vì lý do khách quan",
            //     "Hủy Vé",
            //     "");
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Dispose();
            return true;

        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new ExceptionDetail("Errpr");
            _unitOfWork.Dispose();
        }
    }

    public async Task<bool> Create(TicketFormCreate entity, int userId)
    {
        if (await CheckTicketIsExist(entity.BusId, entity))
            throw new ExceptionDetail(TicketConstants.TICKET_EXIST);
        await _unitOfWork.BeginTransaction();
        try
        {
            //Create Ticket
            Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
            
            PriceClassificationResponse priceClassificationResponse =
                await _priceClassification.GetById(entity.PriceClassificationId);
            RouteDetailResponse response = await _routeDetail.GetById(entity.TicketStations[0].RouteDetailId);
            PriceResponse priceResponse = await _priceService.GetInRoute(response.RouteId, userId);

            ticket.Date = new DateTime(
                entity.DateOnly.Year, entity.DateOnly.Month, entity.DateOnly.Day, response.DepartureTime.Hours,
                response.DepartureTime.Minutes, response.DepartureTime.Microseconds);
            ticket.Status = (int)EnumsApp.Active;
            ticket.PriceClassification = new PriceClassification
            {
                Id = entity.PriceClassificationId
            };
            ticket = await _repository.Create(ticket, userId);

            // Ticket_BusStop ticketBusStop = new Ticket_BusStop();
            // foreach (var ticketStation in entity.TicketStations)
            // {
            //     ticketBusStop = _mapper.Map<Ticket_BusStop>(ticketStation);
            //     ticketBusStop.Status = (int)EnumsApp.Active;
            //     ticketBusStop.Ticket = new Core.Models.Entity.Ticket
            //     {
            //         Id = ticket.Id
            //     };
            //     await _ticketBusStop.Create(ticketBusStop, userId);
            // }
            int count = 0;
            foreach (var ticketStation in entity.TicketStations)
            {
                Ticket_RouteDetail ticketRouteDetail = new Ticket_RouteDetail();
                ticketRouteDetail.RouteDetail = new RouteDetail
                {
                    Id = ticketStation.RouteDetailId
                };
                ticketRouteDetail.Ticket = new Core.Models.Entity.Ticket
                {
                    Id = ticket.Id
                };
                ticketRouteDetail.Status = (int)EnumsApp.Active;
                
                RouteDetailResponse routeDetailResponse = await _routeDetail.GetById(ticketStation.RouteDetailId);
                DateOnly date = entity.DateOnly;
                date = date.AddDays(routeDetailResponse.AddDay);
                
                ticketRouteDetail.DepartureTime = new DateTime(date.Year, date.Month, date.Day,
                    routeDetailResponse.DepartureTime.Hours, routeDetailResponse.DepartureTime.Minutes,
                    routeDetailResponse.DepartureTime.Microseconds);
                
                ticketRouteDetail.ArrivalTime = new DateTime(date.Year, date.Month, date.Day,
                    routeDetailResponse.ArrivalTime.Hours, routeDetailResponse.ArrivalTime.Minutes,
                    routeDetailResponse.ArrivalTime.Microseconds);

                if (count == 0)
                {
                    ticketRouteDetail.ArrivalTime = default;
                }

                if (count == entity.TicketStations.Count -1)
                {
                    ticketRouteDetail.DepartureTime = default;
                }

                count++;
                await _ticketRouteDetail.Create(ticketRouteDetail, userId);
            }

            
            
            // Create TicketItem
            TicketSpecification ticketSpecification = new TicketSpecification(ticket.Id, checkStatus: false, getIsChangeStatus: false);
            BusSpecification busSpecification = new BusSpecification(entity.BusId);
            Bus bus = await _busRepository.Get(busSpecification);
            ticket = await _repository.Get(ticketSpecification);
            ticket.Bus = bus;
            List<Seat> seats = ticket.Bus.Seats.ToList();
            foreach (Seat seat in seats)
            {
                if (seat.Status != (int)EnumsApp.Active && seat.Status != (int)EnumsApp.AwaitingPayment)
                    throw new ExceptionDetail();
                double price = seat.Price;
                price += (priceResponse.Price + priceResponse.Price * priceClassificationResponse.Value/100);
                await CreateItem(seat, ticket.Id, userId, (int)price);
            }

            await SendMail(
                ticket,
                $"Bạn vừa tạo một vé cho ngày: {ticket.Date} cho xe: {ticket.Bus.BusNumber}",
                $"Vé vừa tạo",
                $"");
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e.ToString());
            throw new ExceptionDetail(TicketConstants.ERROR);
        }
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

    public Task<TicketPagingResult> GetAllByAdmin(TicketPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<TicketPagingResult> GetAll(TicketPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketPagingResult> GetAll(TicketPaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification(companyId:idMaster, checkStatus:checkStatus, paging: pagingRequest);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(idMaster, paging:null));
        List<TicketResponse> responses = new List<TicketResponse>();
        foreach (var item in tickets)
        {
            TicketResponse ticketResponse = await GetById(item.Id);
            if (ticketResponse == null)
            {
                continue;
            }
            responses.Add( ticketResponse);
        }
        TicketPagingResult result = AppUtils.ResultPaging<TicketPagingResult, TicketResponse>(
            pagingRequest.PageIndex,
            pagingRequest.PageSize,
            count,
            responses);
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketPagingResult> FindByParam(string param, TicketPaging pagingRequest = default, bool checkStatus = true)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketPagingResult> GetAllTicket(SearchForm searchForm, TicketPaging paging)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(searchForm.StationStart, searchForm.StationEnd, searchForm.DateTime,paging:paging,
            companyIds:searchForm.CompanyIds, priceIsDesc: searchForm.PriceIsDesc, timeInDays:searchForm.TimeInDays);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(searchForm.StationStart, searchForm.StationEnd, searchForm.DateTime,
            companyIds:searchForm.CompanyIds, priceIsDesc: searchForm.PriceIsDesc, timeInDays:searchForm.TimeInDays));
        // Find
        List<TicketResponse> responses = new List<TicketResponse>();
        
        foreach (var item in tickets)
        {
            responses.Add(await GetById(item.Id));
        }

        TicketPagingResult result = AppUtils.ResultPaging<TicketPagingResult, TicketResponse>(
            paging.PageIndex, paging.PageSize, count: count, responses);
        return result;
    }

    public async Task<bool> ChangeCompleteStatus(int id, int userId)
    {
        try
        {
            await _unitOfWork.BeginTransaction();
            TicketSpecification ticketSpecification = new TicketSpecification(id: id, checkStatus: false, getIsChangeStatus:true, userId:userId);
            Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification, checkStatus: false);
            TicketItemSpecification ticketItemSpecification =
                new TicketItemSpecification(0, id, true, checkStatus: false);
            List<TicketItem> items = await _ticketItemRepository.ToList(ticketItemSpecification);
            ticket.TicketItems = new HashSet<TicketItem>(items);
            await _repository.ChangeStatus(ticket, userId, (int)EnumsApp.Complete);

            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Dispose();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
             _unitOfWork.Dispose();
        }
        return true;
    }

    public async Task<TicketPagingResult> GetAllTicketOnDate(int idMaster, DateOnly date, TicketPaging paging)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification(companyId:idMaster, checkStatus:false, paging: paging);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(companyId:idMaster, checkStatus:false));
        // Find
        List<TicketResponse> responses = new List<TicketResponse>();
        
        foreach (var item in tickets)
        {
            responses.Add(await GetById(item.Id));
        }

        TicketPagingResult result = AppUtils.ResultPaging<TicketPagingResult, TicketResponse>(
            paging.PageIndex, paging.PageSize, count: count, responses);
        return result;
    }

    public async Task<TicketPagingResult> GetAll(DateOnly month, int companyId, TicketPaging paging)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification(companyId:companyId, checkStatus:false, paging: paging, month: month);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(companyId:companyId, checkStatus:false, month: month));
        // Find
        List<TicketResponse> responses = new List<TicketResponse>();
        
        foreach (var item in tickets)
        {
            responses.Add(await GetById(item.Id));
        }

        TicketPagingResult result = AppUtils.ResultPaging<TicketPagingResult, TicketResponse>(
            paging.PageIndex, paging.PageSize, count: count, responses);
        return result;
    }

    public async Task<List<Core.Models.Entity.Ticket>> DepartureBeforeMinute(int minute, bool checkStatus = false)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification();
        ticketSpecification.DepartureBeforeMinute(minute, false);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        return tickets;
    }

    public async Task<List<Core.Models.Entity.Ticket>> TicketComplete(bool checkStatus = false)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification();
        ticketSpecification.CompleteTicket();
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        return tickets;
    }

    #region -- Private Method --

    private async Task<bool> CreateItem(Seat seat, int ticketID, int userId, int price)
    {
        TicketItemForm form = new TicketItemForm();
        form.ticketID = ticketID;
        form.status = seat.Status;
        form.ticketItemID = 0;
        form.seatNumber = seat.SeatNumber;
        form.price = seat.Price + price;

        return await _itemService.Create(form, userId);
    }

    private async Task<List<StationResponse>> GetAllBusStopInTicket(int ticketId)
    {
        TicketRouteDetailSpec ticketBusStopSpecification = new TicketRouteDetailSpec(ticketId: ticketId);
        List<Ticket_RouteDetail> ticketRouteDetails = await _ticketRouteDetail.ToList(ticketBusStopSpecification);

        List<StationResponse> stationResponses = new List<StationResponse>();
        StationResponse stationResponse = new StationResponse();
        foreach (var item in ticketRouteDetails)
        {
            stationResponse = _mapper.Map<StationResponse>(item);
            stationResponse.Address = item.RouteDetail.Station.Address + " "+  await AddressUtils.GetAddressDb(item.RouteDetail.Station.Ward.Id, _wardService);
            stationResponses.Add(stationResponse);
        }

        return stationResponses;
    }

    private async Task<bool> CheckTicketIsExist(int busId, TicketFormCreate request)
    {
        int stationStartId = request.TicketStations[0].RouteDetailId;
        RouteDetailResponse detailResponse = await _routeDetail.GetById(stationStartId);
        DateTime departureTime = new DateTime(
            request.DateOnly.Year, request.DateOnly.Month, request.DateOnly.Day,
            detailResponse.DepartureTime.Hours, detailResponse.DepartureTime.Minutes,
            detailResponse.DepartureTime.Microseconds);
        TicketSpecification ticketSpecification = new TicketSpecification(busId: busId, departureTime: departureTime);
        bool status = await _repository.Contains(ticketSpecification);
        return status;
    }
    
    private async Task<bool> SendMail(Core.Models.Entity.Ticket ticket, string message, string subject, string content)
    {
        Company company = ticket.Bus.Company;
        MailRequest mailRequest = new MailRequest();
        mailRequest.ToMail = company.Email;
        mailRequest.Message = message;
        mailRequest.Content = content;
        mailRequest.Subject = subject;
        mailRequest.FullName = company.Name;
        await _mailService.SendEmailAsync(mailRequest);
        return true;
    }
    
    private async Task<bool> SendMails(Core.Models.Entity.Ticket ticket, string message, string subject, string content)
    {
            List<int> checkBillId = new List<int>();
            List<MailRequest> mailRequests = new List<MailRequest>();
            foreach (var ticketItem in ticket.TicketItems)
            {
                if ( checkBillId.Contains(ticketItem.BillItem.Bill.Id))
                {
                    checkBillId.Add(ticketItem.BillItem.Bill.Id);
                    var customer = ticketItem.BillItem.Bill.Customer;
                    MailRequest mailRequest = new MailRequest();
                    mailRequest.ToMail = customer.Email;
                    mailRequest.Message = message;
                    mailRequest.Content = content;
                    mailRequest.Subject = subject;
                    mailRequest.FullName = customer.FullName;
                    
                    mailRequests.Add(mailRequest);
                }
            }

            await _mailService.SendEmailsAsync(mailRequests);
            return true;
    }
    #endregion -- Private Method --
}