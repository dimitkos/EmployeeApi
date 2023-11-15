using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.DatabaseContext
{
    class MigrationsContextFactory : IDesignTimeDbContextFactory<EmployeeDbContext>
    {
        public EmployeeDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(@"../Api/"))
                .AddJsonFile("appsettings.json")
                //.AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var builder = new DbContextOptionsBuilder<EmployeeDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString(Constants.Databases.Employees));

            return new EmployeeDbContext(builder.Options);
        }
    }
}
