using BusBookTicket.Application.CloudImage.Repositories;
using BusBookTicket.Application.CloudImage.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BusBookTicket.Application.CloudImage.Services;

public class ImageService : IImageService
{

    #region -- Properties --
    private readonly ClouImageCore _cloudImage;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Images> _repository;
    #endregion -- Properties --

    public ImageService(ClouImageCore cloudImage, IUnitOfWork unitOfWork)
    {
        this._cloudImage = cloudImage;
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GenericRepository<Images>();
    }
    public async Task<bool> saveImage(IFormFile file, string objectModel, int key)
    {
        string imageResult = await _cloudImage.SaveImage(file);
        if (imageResult == null)
            return false;
        Images images = new Images
        {
            image = imageResult,
            id01 = key,
            objectModel = objectModel
        };
        await _repository.Create(images, -1);
        return true;
    }

    public async Task<List<string>> getImages(string objectModel, int key)
    {
        ImageSpecification imageSpecification = new ImageSpecification(objectModel, key);
        List<Images> imagesList = await _repository.ToList(imageSpecification);
        List<string> responses = new List<string>();
        foreach (Images images in imagesList)
        {
            responses.Add(images.image);
        }

        return responses;
    }
    
}