using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Contexts;

public class ETicaretDbContext : DbContext
{
    public ETicaretDbContext(DbContextOptions options) : base(options)
    {
        // IoC Container'a DbContext'i vericez. 
        // IoC Container'dan bunu talep edebilmek için belirli konfigürasyonlar sağlanmalı.
        // Bu ayarları base class'ımızda var. Ondan dolayı bir constructor'da bunu base class'a iletmeliyiz. 
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }


    // Repo'da kullandığımız fonksiyonu override etmemiz gerek 4 Overloadı var. 4302
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // ChangeTracker.Entries() -> Tüm izlenen entityleri verir. -> IEnumerable<EntityEntry>
        // ChangeTracker.Entries<T>() -> T tipinde izlenenleri verir. IEnumerable<EntityEntry<T>>
        var softEntities = ChangeTracker.Entries<ISoftDelete>();
        var timeStampEntities = ChangeTracker.Entries<ITimeStamp>();

        foreach (var data in softEntities)
        {
            switch (data.State)
            {
                case EntityState.Deleted:
                    data.Entity.IsDeleted = true;
                    data.Entity.IsDeletedAt = DateTime.UtcNow;
                    data.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    data.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Added:
                    data.Entity.CreatedAt = DateTime.UtcNow;
                    data.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        foreach (var entityEntry in timeStampEntities)
        {
            switch (entityEntry.State)
            {
                case EntityState.Modified:
                    entityEntry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Added:
                    entityEntry.Entity.CreatedAt = DateTime.UtcNow;
                    entityEntry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}