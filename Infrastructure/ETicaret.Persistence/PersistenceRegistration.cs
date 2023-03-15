using ETicaret.Application.Repositories.Customer;
using ETicaret.Application.Repositories.Order;
using ETicaret.Application.Repositories.Product;
using ETicaret.Application.Repositories.ProductImages;
using ETicaret.Domain.Entities.Identity;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.ExecutionStrategies;
using ETicaret.Persistence.Repositories.Customer;
using ETicaret.Persistence.Repositories.Order;
using ETicaret.Persistence.Repositories.Product;
using ETicaret.Persistence.Repositories.ProductImage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaret.Persistence;

public static class PersistenceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services)
    {
        services.AddDbContext<ETicaretDbContext>(options =>
        {
            options.UseSqlServer(PersistenceConfig.ConnectionString, sql =>
            {
                sql.MigrationsAssembly(typeof(ETicaretDbContext).Assembly.FullName);
                sql.CommandTimeout((int)TimeSpan.FromSeconds(30).TotalSeconds); // Bir executenın Kaç saniye süreceği
                sql.ExecutionStrategy(dependencies => new ETicaretExecutionStrategy(dependencies,
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30)
                ));
            });
        });
        services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ETicaretDbContext>();

        // Repositories
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
        services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
    }
}