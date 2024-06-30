using AutoMapper;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Paging;
using BusBookTicket.Application.Notification.Specification;
using BusBookTicket.Core.Infrastructure.Dapper;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BusBookTicket.Application.Notification.Services;

public class NotificationService : INotificationService
{
    #region -- Properties --

    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<NotificationChange> _repositoryNotificationChange;
    private readonly IGenericRepository<NotificationObject> _repositoryNotificationObject;
    private readonly IGenericRepository<Core.Models.Entity.Notification> _repositoryNotification;
    private readonly IConfiguration _configuration;
    private readonly IDapperContext<int> _dapper;


    #endregion -- Properties --
    
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext, IMapper mapper, IConfiguration configuration, IDapperContext<int> dapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryNotificationChange = _unitOfWork.GenericRepository<NotificationChange>();
        _repositoryNotification = _unitOfWork.GenericRepository<Core.Models.Entity.Notification>();
        _repositoryNotificationObject = unitOfWork.GenericRepository<NotificationObject>();
        _hubContext = hubContext;
        _mapper = mapper;
        _configuration = configuration;
        _dapper = dapper;
    }
    
    public async Task InsertNotification(AddNewNotification request, int userId)
    {
        
        NotificationChange notificationChange = _mapper.Map<NotificationChange>(request);
        NotificationObject notificationObject = _mapper.Map<NotificationObject>(request);
        Core.Models.Entity.Notification notification = _mapper.Map<Core.Models.Entity.Notification>(request);
        notificationObject.Status = (int)EnumsApp.NotSeen;
        
        notification = await _repositoryNotification.Create(notification, userId);
        notificationObject.Notifications = new HashSet<Core.Models.Entity.Notification> { notification };

        notificationChange = await _repositoryNotificationChange.Create(notificationChange, userId);
        notificationObject.NotificationChanges = new HashSet<NotificationChange> { notificationChange };
        
        notificationObject = await _repositoryNotificationObject.Create(notificationObject, userId);

        int countNotificationNotSeen =
            await _repositoryNotificationObject.Count(new NotificationObjectSpecification(actor: request.Actor, query: "COUNT_NOTIFICATION_NOT_SEEN"));

        await _hubContext.Clients.Group(request.Actor)
            .SendAsync("ReceiveNotification", $" {request.Content}", countNotificationNotSeen, request.Href, notificationObject.Id);
    }

    public Task UpdateNotification(int id, int userId)  
    {
        throw new NotImplementedException();
    }

    public async Task<NotificationResponse> SeenNotification(int id, int userId)
    {
        NotificationObjectSpecification notificationObjectSpecification = new NotificationObjectSpecification(id:id);
        NotificationObject notificationObject = await _repositoryNotificationObject.Get(notificationObjectSpecification);
        notificationObject.Status = (int)EnumsApp.Seen;
        await _repositoryNotificationObject.Update(notificationObject, userId: userId);
        return _mapper.Map<NotificationResponse>(notificationObject);
    }

    public async Task<NotificationPagingResult> GetNotification(string actor)
    {
        NotificationPaging paging = new NotificationPaging();
        NotificationObjectSpecification notificationObjectSpecification = new NotificationObjectSpecification(paging: paging, actor: actor, checkStatus:false);
        int count = await _repositoryNotificationObject.Count(notificationObjectSpecification);
        List<NotificationObject> notificationObjects = await _repositoryNotificationObject.ToList(notificationObjectSpecification);
        
        var result = AppUtils.ResultPaging<NotificationPagingResult, NotificationResponse>(
            index: paging.PageIndex, size: paging.PageSize, 
            items: await AppUtils.MapObject<NotificationObject, NotificationResponse>(notificationObjects, _mapper), 
            count: count);
        return result;
    }

    public async Task AddGroup(string connectionId, string group)
    {
        await _hubContext.Groups.AddToGroupAsync(connectionId, group);
    }
    
    public async Task RemoveGroup(string connectionId, string group)
    {
        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, group);
    }

    public async Task<List<int>> LoadGroup(int userId, bool checkStatus = true)
    {
        string connectionString = _configuration["ConnectionStrings:DefaultDB"];
        string query = @"
            SELECT Distinct(T.TicketId) FROM TicketRouteDetails T RIGHT JOIN Bills B on T.Id = B.TicketRouteDetailStartId
            WHERE B.CustomerID = @customerId and T.arrivalTime>= GetDate()";

        var result = await _dapper.ExecuteQueryAsync(query, new { customerId = userId });
        return result;
    }
}