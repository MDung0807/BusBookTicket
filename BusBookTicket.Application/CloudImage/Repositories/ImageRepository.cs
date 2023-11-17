using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Application.CloudImage.Repositories;

public class ImageRepository : IImageRepository
{
    #region --Properties --

    private readonly AppDBContext _context;
    #endregion --Properties --

    public ImageRepository(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<int> create(Images entity)
    {
        try
        {
            await _context.Images.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }
        catch
        {
            throw new Exception("ERROR");
        }
    }

    public async Task<List<Images>> getAllImage(string objectModel, int key)
    {
        try
        {
            return await _context.Images
                .Where(x => x.ObjectModel == objectModel)
                .Where(x => x.Id01 == key)
                .ToListAsync();
        }
        catch
        {
            throw new Exception("ERROR");
        }
    }
}