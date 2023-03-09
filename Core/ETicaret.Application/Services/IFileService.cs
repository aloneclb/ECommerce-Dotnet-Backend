using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Services;

public interface IFileService
{
    Task<string> Upload(string name, IFormFile imageFile);
    void Delete(string imageName);
}