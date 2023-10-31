using Microsoft.AspNetCore.Http;

namespace BusBookTicket.Application.CloudImage.Services;

public interface IImageService
{
    Task<bool> saveImage(IFormFile file, string objectModel, int key);
    Task<List<string>> getImages(string objectModel, int key);
}