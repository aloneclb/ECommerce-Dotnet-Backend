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
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    
    // Repo'da kullandığımız fonksiyonu override etmemiz gerek 4 Overloadı var. 4302
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // ChangeTracker.Entries() -> Tüm izlenen entityleri verir. -> IEnumerable<EntityEntry>
        // ChangeTracker.Entries<T>() -> T tipinde izlenenleri verir. IEnumerable<EntityEntry<T>>
        var datas = ChangeTracker.Entries<ISoftDelete>();

        foreach (var data in datas)
        {
            switch(data.State)
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
            };
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}