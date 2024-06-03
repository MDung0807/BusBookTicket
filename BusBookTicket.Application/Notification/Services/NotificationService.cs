using AutoMapper;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Paging;
using BusBookTicket.Application.Notification.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification.Services;

public class NotificationService : INotificationService
{
    #region -- Properties --

    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<NotificationChange> _repositoryNotificationChange;
    private readonly IGenericRepository<NotificationObject> _repositoryNotificationObject;
    private readonly IGenericRepository<Core.Models.Entity.Notification> _repositoryNotification;

    #endregion -- Properties --
    
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IHubContext<NotificationHub> hubContext, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repositoryNotificationChange = _unitOfWork.GenericRepository<NotificationChange>();
        _repositoryNotification = _unitOfWork.GenericRepository<Core.Models.Entity.Notification>();
        _repositoryNotificationObject = unitOfWork.GenericRepository<NotificationObject>();
        _hubContext = hubContext;
        _mapper = mapper;
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
        
        await _repositoryNotificationObject.Create(notificationObject, userId);

        int countNotificationNotSeen =
            await _repositoryNotificationObject.Count(new NotificationObjectSpecification(actor: $"{AppConstants.ADMIN}_1", query: "COUNT_NOTIFICATION_NOT_SEEN"));

        await _hubContext.Clients.Group($"{AppConstants.ADMIN}_1")
            .SendAsync("ReceiveNotification", $"{request.Sender} {request.Content}", countNotificationNotSeen, request.Href);
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
}