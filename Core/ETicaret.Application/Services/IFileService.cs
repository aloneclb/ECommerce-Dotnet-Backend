using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Services;

public interface IFileService
{
    Task<string> UploadAsync(string name, IFormFile imageFile);
    void Delete(string imageName);
}