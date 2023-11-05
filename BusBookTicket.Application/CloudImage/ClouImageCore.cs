using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Account = CloudinaryDotNet.Account;

namespace BusBookTicket.Application.CloudImage;

public class ClouImageCore
{
    private readonly Cloudinary _cloudinary  = new Cloudinary(
        new Account("dx7nsygei", 
            "889198864823844", 
            "m1llh5b2a2t_1JaApGYaXcjiL3w"));
    
    /// <summary>
    /// Upload image param file to Clounddinary.
    /// Link: https://console.cloudinary.com/pm/c-966abc8ff388285b75470c912148cb/getting-started
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<string> SaveImage(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = file.FileName
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.Url.ToString();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }
}