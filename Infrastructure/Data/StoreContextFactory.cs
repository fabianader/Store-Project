using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StoreProject.Infrastructure.Data;

namespace StoreProject.Infrastructure.Data
{
    public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
    {
        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlServer("Default");

            return new StoreContext(optionsBuilder.Options);
        }
    }
}
