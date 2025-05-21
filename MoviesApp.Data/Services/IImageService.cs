using Microsoft.AspNetCore.Http;
namespace MoviesApp.Core.Services
{
    public interface IImageService 
    {
         Task<string> AddMedia(IFormFile file , string mediaType);
         void DeleteMedia(string src);
    }
}
