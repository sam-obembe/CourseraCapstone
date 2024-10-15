// See https://aka.ms/new-console-template for more information

using Capstone.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString = context.Configuration.GetConnectionString("Database");
                services.AddDbContext<CapstoneContext>(options => options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0)),builder => builder.MigrationsAssembly("Capstone.Migrator"))
                );
            })
            .Build();
        
        await host.RunAsync();
    }
}