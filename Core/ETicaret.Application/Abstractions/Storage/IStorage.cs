using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Abstractions.Storage;

public interface IStorage
{
    Task<string> UploadAsync(string fileName, IFormFile imageFile);
    Task DeleteAsync(string fileName);
    List<string> GetAllFiles();
    bool HasFile(string fileName);
}