using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Services.TicketServices;

namespace BusBookTicket.Ticket.Services.BackgroundService;

public class TicketBackgroundService: Microsoft.Extensions.Hosting.BackgroundService, ITicketBackgroundService
{
    private readonly ITicketService _service;
    private readonly INotificationService _notificationService;
    private readonly IGenericRepository<Core.Models.Entity.Ticket> _repository;


    public TicketBackgroundService(ITicketService service, INotificationService notificationService, IUnitOfWork unitOfWork)
    {
        _service = service;
        _notificationService = notificationService;
        _repository = unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
    
    }

    public TicketBackgroundService(){}

    public async Task ChangeIsWaitingTicket()
    {
        List<Core.Models.Entity.Ticket> tickets =  await _service.DepartureBeforeMinute(0);
        await _repository.ChangeStatus(tickets, 0, (int)EnumsApp.Waiting);
    }

    public async Task ChangeIsCompleteTicket()
    {
        List<Core.Models.Entity.Ticket> tickets = await _service.TicketComplete();
        await _repository.ChangeStatus(tickets, 0, (int)EnumsApp.Complete);
    }

    public async Task NotificationBefore6Hour()
    {
        int minute = 6* 60; //Convert from Hour to Minute
        List<Core.Models.Entity.Ticket> tickets =  await _service.DepartureBeforeMinute(minute);
        foreach (var ticket in tickets)
        {
            string content = $"Còn 6h nữa chuyến từ .. tới .. sẽ khởi hành";
            await SendNotification(content, $"TICKET_{ticket.Id}", ticket.Bus.Company.Name, "", 0);
        }
    }

    public async Task NotificationBefore24Hour()
    {
        int minute = 24 * 60; //Convert from Hour to Minute
        List<Core.Models.Entity.Ticket> tickets =  await _service.DepartureBeforeMinute(minute);
        foreach (var ticket in tickets)
        {
            string content = $"Còn 24h nữa chuyến từ .. tới .. sẽ khởi hành";
            await SendNotification(content, $"TICKET_{ticket.Id}", ticket.Bus.Company.Name, "", 0);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.WhenAll(
            NotificationBefore6Hour(), 
            NotificationBefore24Hour(),
            ChangeIsWaitingTicket(),
            ChangeIsCompleteTicket());
        await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
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
}