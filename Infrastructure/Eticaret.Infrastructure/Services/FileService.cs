using ETicaret.Application.Services;
using Eticaret.Infrastructure.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eticaret.Infrastructure.Services;

public class FileService : IFileService
{
    // Todo: Log
    // Todo: Servisi uyarıcı exception mekanizması
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private string wwwroot => _webHostEnvironment.WebRootPath;

    private const string ImgFolder = "images";
    // private const string UserImagesFolder = "user-images";

    public async Task<string> UploadAsync(string name, IFormFile imageFile)
    {
        var pathToSaveFolder = $"{wwwroot}/{ImgFolder}/example";

        // Resimlerin Kaydedileceği dosya yok ise oluşturuyoruz.
        if (!Directory.Exists(pathToSaveFolder))
            Directory.CreateDirectory(pathToSaveFolder);

        var fileExtensions = Path.GetExtension(imageFile.FileName); // Yüklenen Dosyanın Türü

        var pathAndName = await FileRenameAsync(pathToSaveFolder, name, fileExtensions);

        await using var stream = new FileStream(pathAndName, FileMode.Create, FileAccess.Write, FileShare.None,
            1024 * 1024, false);
        await imageFile.CopyToAsync(stream);
        await stream.FlushAsync();

        return pathAndName;
    }

    private async Task<string> FileRenameAsync(string filePath, string fileName, string extension)
    {
        var newFileFullPath = await Task.Run(async () =>
        {
            var newFileFullPath = await RenameFileName(filePath, fileName, extension); // wwwroot/example/imagename.jpg
            var imageCount = 0;

            while (File.Exists(newFileFullPath))
            {
                imageCount += 1;
                newFileFullPath = await RenameFileName(filePath, fileName + imageCount.ToString(), extension);
            }

            return newFileFullPath;
        });

        return newFileFullPath;
    }

    private async Task<string> RenameFileName(string filePath, string fileName, string extension)
    {
        var fullFilePath = await Task.Run(() =>
        {
            var newFileName = NameChanger.ReplaceInvalidChars(fileName); // urunismi
            var newFilePath = Path.Combine(filePath, newFileName); // wwwroot/example/urunismi
            var newFileFullPath = $"{newFilePath}{extension}"; // wwwroot/example/urunismi.jpg
            return Task.FromResult(newFileFullPath);
        });
        return fullFilePath;
    }


    public void Delete(string imageName)
    {
        var path = Path.Combine($"{wwwroot}/{ImgFolder}/{imageName}");
        if (File.Exists(path))
            File.Delete(path);
    }
}