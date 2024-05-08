using AutoMapper;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using Microsoft.AspNetCore.SignalR;

namespace BusBookTicket.Application.Notification.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<NotificationChange> _repositoryNotificationChange;
    private readonly IGenericRepository<NotificationObject> _repositoryNotificationObject;
    private readonly IGenericRepository<Core.Models.Entity.Notification> _repositoryNotification;
    
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
        
        
        await _repositoryNotificationChange.Create(_mapper.Map<NotificationChange>(request), userId);
        await _repositoryNotificationObject.Create(notificationObject, userId);
        await _repositoryNotification.Create(notification, userId);

        int countNotificationNotSeen =
            await _repositoryNotificationChange.Count(new NotificationChangSpecification($"{AppConstants.ADMIN}_1"));

        await _hubContext.Clients.Group($"{AppConstants.ADMIN}_1")
            .SendAsync("ReceiveNotification", $"{request.Sender} {request.Content}", countNotificationNotSeen, request.Href);
    }

    public Task UpdateNotification(int id, int userId)  
    {
        throw new NotImplementedException();
    }
}