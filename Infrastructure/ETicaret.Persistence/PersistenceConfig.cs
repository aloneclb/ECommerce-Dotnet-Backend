using Microsoft.Extensions.Configuration;

namespace ETicaret.Persistence;

static class PersistenceConfig
{
    /// <summary>
    /// Bu metod Extension.Configuration ve ExtensionConfiguration.Json
    /// kütüphanelerini kullanarak dosyanın fiziksel konumundan
    /// connection string'i çeker.
    /// </summary>
    public static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaret.API"));
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("SQLServer")!;
        }
    }
}