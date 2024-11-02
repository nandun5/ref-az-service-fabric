using Microsoft.Extensions.DependencyInjection;

namespace Nandun.Reference.ServiceFabric.Sample.DependencyInjection;

/// <summaNandun
/// Dependency injection for Transaction order channel processors
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the dependencies required for the Processor
    /// </summary>
    /// <param name="services"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddProcessor(
        this IServiceCollection services)
    {
        services.AddTransient<SampleProcessor>();

        return services;
    }

    /// <summary>
    /// Adds the dependencies required for the API
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns></returns>
    public static IServiceCollection AddApi(
        this IServiceCollection services)
    {

        return services;
    }

}
