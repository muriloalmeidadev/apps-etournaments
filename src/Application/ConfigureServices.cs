using Fwks.Core;
using Caesareum.ETournamentsApp.Application.Services;
using Caesareum.ETournamentsApp.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Caesareum.ETournamentsApp.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICustomerService, CustomerService>()
            .AddNotificationContext();
    }
}