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
    public async Task<string> create(Images entity)
    {
        try
        {
            string id = _context.Images.AddAsync(entity).Result.Entity.id;
            await _context.SaveChangesAsync();
            return id;
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
                .Where(x => x.objectModel == objectModel)
                .Where(x => x.id01 == key)
                .ToListAsync();
        }
        catch
        {
            throw new Exception("ERROR");
        }
    }
}