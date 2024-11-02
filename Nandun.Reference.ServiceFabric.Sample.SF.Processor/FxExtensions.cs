using System.Fabric;
using Microsoft.Extensions.Configuration;
using Nandun.Reference.ServiceFabric.Sample.Processor;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides a set of static methods for adding and configuring .NET SDK level components.
/// </summary>
internal static class FxExtensions
{
    /// <summary>
    /// Builds an <see cref="StatelessServiceContext" /> configuration with keys provided from the set of sources registered.
    /// </summary>
    /// <param name="builder">An <see cref="IConfigurationBuilder" /> to build <see cref="IConfiguration" />.</param>
    /// <returns>A <see cref="IConfiguration" /> that contains keys and values.</returns>
    public static IConfiguration Configure(this StatelessServiceContext context, IConfigurationBuilder builder)
    => builder.AddServiceFabricConfiguration(context).AddEnvironmentVariables().Build();


    public static IConfigurationBuilder AddServiceFabricConfiguration(
        this IConfigurationBuilder builder,
        ServiceContext serviceContext)
    {
        builder.Add((IConfigurationSource)new ServiceFabricConfigurationSource(serviceContext));
        return builder;
    }

}
