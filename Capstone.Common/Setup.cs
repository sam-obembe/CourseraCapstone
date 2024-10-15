using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Capstone.Common;

public static class Setup
{
    public static void SetupDatabase(this IHostApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddDbContext<CapstoneContext>(options => options.UseMySql(connectionString,new MySqlServerVersion(new Version(8, 0))));
    }

    public static void SetupDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CapstoneContext>(options => options.UseMySql(connectionString,new MySqlServerVersion(new Version(8, 0))));
    }
}