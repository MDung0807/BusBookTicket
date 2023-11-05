using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Application.CloudImage.Repositories;

public interface IImageRepository
{
    Task<int> create(Images entity);
    Task<List<Images>> getAllImage(string objectModel, int key);
}