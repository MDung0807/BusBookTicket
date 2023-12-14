using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using BusBookTicket.AddressManagement.Services.WardService;
using BusBookTicket.AddressManagement.Utilities;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Buses.Specification;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Paging;
using BusBookTicket.Ticket.Services.TicketItemServices;
using BusBookTicket.Ticket.Specification;
using BusBookTicket.Ticket.Utils;

namespace BusBookTicket.Ticket.Services.TicketServices;

public class TicketService : ITicketService
{
    #region -- Properties --

    private readonly ITicketItemService _itemService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Core.Models.Entity.Ticket> _repository;
    private readonly IGenericRepository<Bus> _busRepository;
    private readonly IGenericRepository<Ticket_BusStop> _ticketBusStop;
    private readonly IGenericRepository<TicketItem> _ticketItemRepository;
    private readonly IImageService _imageService;
    private readonly IWardService _wardService;
    #endregion -- Properties --

    public TicketService(
        ITicketItemService itemService, 
        IMapper mapper, 
        IUnitOfWork unitOfWork,
        IImageService imageService,
        IWardService wardService)
    {
        this._itemService = itemService;
        this._mapper = mapper;
        this._unitOfWork = unitOfWork;
        this._repository = unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
        _busRepository = unitOfWork.GenericRepository<Bus>();
        _ticketBusStop = unitOfWork.GenericRepository<Ticket_BusStop>();
        _ticketItemRepository = unitOfWork.GenericRepository<TicketItem>();
        _imageService = imageService;
        _wardService = wardService;
    }
    
    public async Task<TicketResponse> GetById(int id)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(id,checkStatus:true, getIsChangeStatus:false);
        Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
        List<TicketItemResponse> itemResponses = await _itemService.GetAllInTicket(id);
        int totalEmptySeat = 0;
        foreach (var item in itemResponses)
        {
            if (item.Status == 1)
            {
                totalEmptySeat++;
            }
        }
        TicketResponse response = _mapper.Map<Core.Models.Entity.Ticket, TicketResponse>(ticket);
        response.TotalEmptySeat = totalEmptySeat;
        List<string> images = await _imageService.getImages(typeof(Company).ToString(), id);
        response.CompanyLogo = images.Count > 0 ? images[0] : null;
        response.ItemResponses = itemResponses;
        response.ListStation = await GetAllBusStopInTicket(id);
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
        TicketSpecification ticketSpecification =
            new TicketSpecification(id, checkStatus: false, getIsChangeStatus: true);
        Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
        ticket.Status = (int)EnumsApp.Delete;
        await _repository.Delete(ticket, userId);
        return true;
    }

    public async Task<bool> Create(TicketFormCreate entity, int userId)
    {
        if (!await CheckTicketIsExist(entity.BusId, entity.TicketStations))
            throw new ExceptionDetail(TicketConstants.TICKET_EXIST);
        await _unitOfWork.BeginTransaction();
        try
        {
            //Create Ticket
            Core.Models.Entity.Ticket ticket = _mapper.Map<Core.Models.Entity.Ticket>(entity);
            ticket.Date = entity.TicketStations[0].DepartureTime;
            ticket.Status = (int)EnumsApp.Active;
            ticket = await _repository.Create(ticket, userId);

            Ticket_BusStop ticketBusStop = new Ticket_BusStop();
            foreach (var ticketStation in entity.TicketStations)
            {
                ticketBusStop = _mapper.Map<Ticket_BusStop>(ticketStation);
                ticketBusStop.Status = (int)EnumsApp.Active;
                ticketBusStop.Ticket = new Core.Models.Entity.Ticket
                {
                    Id = ticket.Id
                };
                await _ticketBusStop.Create(ticketBusStop, userId);
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
                await CreateItem(seat, ticket.Id, userId, entity.Price);
            }
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

    public async Task<TicketPagingResult> GetAll(TicketPaging pagingRequest, int idMaster)
    {
        TicketSpecification ticketSpecification = 
            new TicketSpecification(companyId:idMaster, checkStatus:false, paging: pagingRequest);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(idMaster, paging:null));
        List<TicketResponse> responses = new List<TicketResponse>();
        foreach (var item in tickets)
        {
            responses.Add( await GetById(item.Id));
        }
        TicketPagingResult result = new TicketPagingResult(pagingRequest.PageIndex, pagingRequest.PageSize, count, responses);
        return result;
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TicketPagingResult> GetAllTicket(SearchForm searchForm, TicketPaging paging)
    {
        TicketSpecification ticketSpecification = new TicketSpecification(searchForm.StationStart, searchForm.StationEnd, searchForm.DateTime,paging);
        List<Core.Models.Entity.Ticket> tickets = await _repository.ToList(ticketSpecification);
        int count = await _repository.Count(new TicketSpecification(searchForm.StationStart, searchForm.StationEnd,
            searchForm.DateTime));
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
            TicketSpecification ticketSpecification = new TicketSpecification(id: id, checkStatus: false, getIsChangeStatus:true);
            Core.Models.Entity.Ticket ticket = await _repository.Get(ticketSpecification);
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
        TicketBusStopSpecification ticketBusStopSpecification = new TicketBusStopSpecification(ticketId);
        List<Ticket_BusStop> ticketBusStop = await _ticketBusStop.ToList(ticketBusStopSpecification);

        List<StationResponse> stationResponses = new List<StationResponse>();
        StationResponse stationResponse = new StationResponse();
        foreach (var item in ticketBusStop)
        {
            stationResponse = _mapper.Map<StationResponse>(item);
            stationResponse.Address = item.BusStop.BusStation.Address+ " "+  await AddressUtils.GetAddressDb(item.BusStop.BusStation.Ward.Id, _wardService);
            stationResponses.Add(stationResponse);
        }

        return stationResponses;
    }

    private async Task<bool> CheckTicketIsExist(int busId, List<TicketStationDto> ticketStationDtos)
    {
        DateTime dePartureTime = ticketStationDtos[0].DepartureTime;
        foreach (var item in ticketStationDtos)
        {
            if (item.DepartureTime < dePartureTime)
                dePartureTime = item.DepartureTime;
        }
        TicketSpecification ticketSpecification = new TicketSpecification(busId: busId, departureTime: dePartureTime);
        bool status = await _repository.Contains(ticketSpecification);
        return !status;
    }
    #endregion -- Private Method --
}