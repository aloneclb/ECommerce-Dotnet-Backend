using ETicaret.Application.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Eticaret.Infrastructure.Services;

public class ImageService : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    private string wwwroot => _webHostEnvironment.WebRootPath;

    private const string ImgFolder = "images";
    // private const string ArticleImagesFolder = "article-images";
    // private const string UserImagesFolder = "user-images";

    public async Task<string> Upload(string name, IFormFile imageFile)
    {
        var fullPath = $"{wwwroot}/{ImgFolder}/example";

        // Resimlerin Kaydedileceği dosya yok ise oluşturuyoruz.
        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        var oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName); // Yüklenen Dosyanın Adı
        var fileExtensions = Path.GetExtension(imageFile.FileName); // Yüklenen Dosyanın Türü

        name = ReplaceInvalidChars(name); // Dosya adını türkçeden ingilizceye çeviriyoruz
        var newFileName = $"{name}_{Guid.NewGuid().ToString()}{fileExtensions}"; // Guid de kullanılabilir.
        var pathAndName = Path.Combine(fullPath, newFileName);

        await using var stream = new FileStream(pathAndName, FileMode.Create, FileAccess.Write, FileShare.None,
            1024 * 1024, false);
        await imageFile.CopyToAsync(stream);
        await stream.FlushAsync();

        return pathAndName;
    }

    public void Delete(string imageName)
    {
        var path = Path.Combine($"{wwwroot}/{ImgFolder}/{imageName}");
        if (File.Exists(path))
            File.Delete(path);
    }

    private string ReplaceInvalidChars(string fileName)
    {
        return fileName.Replace("İ", "I")
            .Replace("ı", "i")
            .Replace("Ğ", "G")
            .Replace("ğ", "g")
            .Replace("Ü", "U")
            .Replace("ü", "u")
            .Replace("ş", "s")
            .Replace("Ş", "S")
            .Replace("Ö", "O")
            .Replace("ö", "o")
            .Replace("Ç", "C")
            .Replace("ç", "c")
            .Replace("é", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("*", "")
            .Replace("æ", "")
            .Replace("ß", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("#", "")
            .Replace("$", "")
            .Replace("½", "")
            .Replace("{", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("}", "")
            .Replace(@"\", "")
            .Replace("|", "")
            .Replace("~", "")
            .Replace("¨", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace("`", "")
            .Replace(".", "")
            .Replace(":", "")
            .Replace(" ", "");
    }
}