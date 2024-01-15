using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Transactions.Application.SeedWork.Behaviors;

namespace Transactions.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(executingAssembly);
            config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        });
        return services;
    }
}
