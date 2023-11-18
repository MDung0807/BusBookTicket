using Microsoft.AspNetCore.Http;

namespace BusBookTicket.Application.CloudImage.Services;

public interface IImageService
{
    Task<bool> saveImage(IFormFile file, string objectModel, int key);
    
    /// <summary>
    /// Get images in object
    /// </summary>
    /// <param name="objectModel">Object</param>
    /// <param name="key">Primary key in object</param>
    /// <returns>
    /// List image in object
    /// </returns>
    Task<List<string>> getImages(string objectModel, int key);
}