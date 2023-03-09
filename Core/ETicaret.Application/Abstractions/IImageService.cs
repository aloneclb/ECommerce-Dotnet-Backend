using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Abstractions;

public interface IImageService
{
    Task<string> Upload(string name, IFormFile imageFile);
    void Delete(string imageName);
}