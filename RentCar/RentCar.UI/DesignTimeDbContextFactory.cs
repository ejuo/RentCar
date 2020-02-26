using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RentCar.DataAccess;
using System.IO;

namespace RentCar.UI
{
    /// <summary>
    /// Implementation for migrations from RentCar.DataAccess
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RentCarDbContext>
    {
        public RentCarDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            var builder = new DbContextOptionsBuilder<RentCarDbContext>();

            var connectionString = configuration["connectionStrings:add:defaultConnection:connectionString"];

            builder.UseSqlServer(connectionString);

            return new RentCarDbContext(builder.Options);
        }
    }
}