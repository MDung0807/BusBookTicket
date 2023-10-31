using BusBookTicket.Application.CloudImage.Repositories;
using BusBookTicket.Core.Models.Entity;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BusBookTicket.Application.CloudImage.Services;

public class ImageService : IImageService
{

    #region -- Properties --
    private readonly ClouImageCore _cloudImage;
    private readonly IImageRepository _imageRepository;    
    #endregion -- Properties --

    public ImageService(ClouImageCore cloudImage, IImageRepository repository)
    {
        this._cloudImage = cloudImage;
        this._imageRepository = repository;
    }
    public async Task<bool> saveImage(IFormFile file, string objectModel, int key)
    {
        string imageResult = await _cloudImage.saveImage(file);
        if (imageResult == null)
            return false;
        Images images = new Images
        {
            image = imageResult,
            id01 = key,
            objectModel = objectModel
        };
        await _imageRepository.create(images);
        return true;
    }

    public async Task<List<string>> getImages(string objectModel, int key)
    {
        List<Images> imagesList = await _imageRepository.getAllImage(objectModel, key);
        List<string> responses = new List<string>();
        foreach (Images images in imagesList)
        {
            responses.Add(images.image);
        }

        return responses;
    }
    
}