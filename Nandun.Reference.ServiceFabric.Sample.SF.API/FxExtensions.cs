using System.Fabric;
using Nandun.Reference.ServiceFabric.Sample.SF.API;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides a set of static methods for adding and configuring .NET SDK level components.
/// </summary>
internal static class FxExtensions
{
    public static IConfigurationBuilder AddServiceFabricConfiguration(
        this IConfigurationBuilder builder,
        ServiceContext serviceContext)
    {
        builder.Add((IConfigurationSource)new ServiceFabricConfigurationSource(serviceContext));
        return builder;
    }

}
