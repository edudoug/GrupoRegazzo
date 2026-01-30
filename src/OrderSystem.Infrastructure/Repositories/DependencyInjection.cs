using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //Repositories
        services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
        //Feature flags
        services.AddSingleton<IFeatureFlagPort, FeatureFlagsAdapter>();
        
        return services;
    }
}