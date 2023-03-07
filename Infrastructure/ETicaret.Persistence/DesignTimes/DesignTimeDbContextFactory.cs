using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ETicaret.Persistence.DesignTimes;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretDbContext>
{
    public ETicaretDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ETicaretDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseSqlServer(PersistenceConfig.ConnectionString);
        return new ETicaretDbContext(dbContextOptionsBuilder.Options);
    }
}