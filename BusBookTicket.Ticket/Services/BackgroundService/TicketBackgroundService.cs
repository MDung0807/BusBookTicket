using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.Services.TicketServices;
using Microsoft.Extensions.Logging;

namespace BusBookTicket.Ticket.Services.BackgroundService;

public class TicketBackgroundService : Microsoft.Extensions.Hosting.BackgroundService, ITicketBackgroundService
{
    private readonly ITicketService _service;
    private readonly INotificationService _notificationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Core.Models.Entity.Ticket> _repository;
    private readonly ILogger<TicketBackgroundService> _logger;

    public TicketBackgroundService(
        ITicketService service, 
        INotificationService notificationService, 
        IGenericRepository<Core.Models.Entity.Ticket> repository, 
        IUnitOfWork unitOfWork,
        ILogger<TicketBackgroundService> logger)
    {
        _service = service;
        _notificationService = notificationService;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
        _logger = logger;
    }

    public async Task ChangeIsWaitingTicket()
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            List<Core.Models.Entity.Ticket> tickets = await _service.DepartureBeforeMinute(0);
            List<object> list = new List<object>(tickets);
            await _repository.ChangeStatus(list, 0, (int)EnumsApp.Waiting);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error in ChangeIsWaitingTicket");
            throw;
        }
    }

    public async Task ChangeIsCompleteTicket()
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            List<Core.Models.Entity.Ticket> tickets = await _service.TicketComplete();
            List<object> list = new List<object>(tickets);
            await _repository.ChangeStatus(list, 0, (int)EnumsApp.Complete);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error in ChangeIsCompleteTicket");
            throw;
        }
    }

    public async Task NotificationBefore6Hour()
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            int minute = 6 * 60; // Convert from Hour to Minute
            List<Core.Models.Entity.Ticket> tickets = await _service.DepartureBeforeMinute(minute);
            foreach (var ticket in tickets)
            {
                string content = $"Còn 6h nữa chuyến từ .. tới .. sẽ khởi hành";
                await SendNotification(content, $"TICKET_{ticket.Id}", ticket.Bus.Company.Name, "", 0);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.BeginTransaction();
            _logger.LogError(ex, "Error in NotificationBefore6Hour");
            throw;
        }
    }

    public async Task NotificationBefore24Hour()
    {
        await _unitOfWork.BeginTransaction();
        try
        {
            int minute = 24 * 60; // Convert from Hour to Minute
            List<Core.Models.Entity.Ticket> tickets = await _service.DepartureBeforeMinute(minute);
            foreach (var ticket in tickets)
            {
                string content = $"Còn 24h nữa chuyến từ .. tới .. sẽ khởi hành";
                await SendNotification(content, $"TICKET_{ticket.Id}", ticket.Bus.Company.Name, "", 0);
            }
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError(ex, "Error in NotificationBefore24Hour");
            throw;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                
                
                await NotificationBefore6Hour();
                await NotificationBefore24Hour();
                await ChangeIsWaitingTicket();
                await ChangeIsCompleteTicket();
                
                
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
            catch (Exception ex)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                _logger.LogError(ex, "Error in ExecuteAsync");
                break;
                // Depending on your needs, you might want to rethrow the exception or just log it and continue
                // throw;
            }
        }
    }

    private async Task SendNotification(string content, string actor, string sender, string href, int userId)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendNotification");
            throw;
        }
    }
}
