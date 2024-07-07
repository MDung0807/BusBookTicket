using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Core.Infrastructure.Dapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
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
    private readonly IMailService _mailService;
    private readonly IDapperContext<Customer> _dapperContext;

    public TicketBackgroundService(
        ITicketService service,
        INotificationService notificationService,
        IGenericRepository<Core.Models.Entity.Ticket> repository,
        IUnitOfWork unitOfWork,
        ILogger<TicketBackgroundService> logger, IMailService mailService, IDapperContext<Customer> dapperContext)
    {
        _service = service;
        _notificationService = notificationService;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<Core.Models.Entity.Ticket>();
        _logger = logger;
        _mailService = mailService;
        _dapperContext = dapperContext;
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
            await SendMailToListCustomer(tickets);
            foreach (var ticket in tickets)
            {
                string content = $"Còn 6h nữa chuyến từ .. tới .. sẽ khởi hành";
                await SendNotification(content, $"{AppConstants.TICKET_GROUP}_{ticket.Id}", ticket.Bus.Company.Name, "", 0);
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
            await SendMailToListCustomer(tickets);
            foreach (var ticket in tickets)
            {
                string content = $"Còn 24h nữa chuyến từ .. tới .. sẽ khởi hành";
                await SendNotification(content, $"{AppConstants.TICKET_GROUP}_{ticket.Id}", ticket.Bus.Company.Name, "",
                    0);
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

    private async Task SendMailToListCustomer(List<Core.Models.Entity.Ticket> tickets)
    {
        if (tickets.Count == 0)
        {
            return;
        }
        List<Task<List<Customer>>> tasks = new List<Task<List<Customer>>>();
        foreach (var ticket in tickets)
        {
            tasks.Add(GetAllCustomerInTicket(ticket.Id));
        }

        List<MailRequest> mailRequests = new List<MailRequest>();
        var result = await Task.WhenAll(tasks);
        foreach (var customers in result)    
        {
            foreach (var customer in customers)
            {
                mailRequests.Add(new MailRequest
                {
                    Content = "Xe sắp xuất bến",
                    FullName = customer.FullName,
                    Message = "Xe xắp xuất bến, quý khách thắt dây an toàn nhá",
                    LinkImage = null,
                    Subject = "Thông báo lịch ",
                    ToMail = customer.Email
                });
            }
        }

        await _mailService.SendEmailsAsync(mailRequests);
    }

    private async Task<List<Customer>> GetAllCustomerInTicket(int ticketId)
    {
        string query = @"
                SELECT DISTINCT C.*
                FROM Tickets T
                    LEFT JOIN TicketItems TI ON T.Id = TI.TicketID
                    LEFT JOIN BillItems BI ON BI.TicketItemID = TI.Id
                    LEFT JOIN Bills B ON B.Id = BI.BillID
                    RIGHT JOIN Customers C ON C.Id = B.CustomerID
                WHERE T.Id = @ticketId";
        var result = await _dapperContext.ExecuteQueryAsync(query, new { ticketId = ticketId });
        return result;
    }
}
