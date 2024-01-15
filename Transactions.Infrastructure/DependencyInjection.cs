using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SQLite;
using Transactions.Application.SeedWork.Interfaces;

namespace Transactions.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string dbConnectionString)
    {
        services.AddScoped<IDbConnection>(_=> new SQLiteConnection($"{dbConnectionString}"));
        services.AddScoped<IDatabaseContext, SqlLiteDatabaseContext>();
        return services;
    }
}
