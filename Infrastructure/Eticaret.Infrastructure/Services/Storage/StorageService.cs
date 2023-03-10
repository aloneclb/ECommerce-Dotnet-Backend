using ETicaret.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace Eticaret.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IStorage _storage; // mimaride kullanılacak storage'ı talep et

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<string> UploadAsync(string fileName, IFormFile imageFile)
    {
        return await _storage.UploadAsync(fileName, imageFile);
    }

    public async Task DeleteAsync(string fileName)
    {
        await _storage.DeleteAsync(fileName);
    }

    public List<string> GetAllFiles()
    {
        return _storage.GetAllFiles();
    }

    public bool HasFile(string fileName)
    {
        return _storage.HasFile(fileName);
    }
}