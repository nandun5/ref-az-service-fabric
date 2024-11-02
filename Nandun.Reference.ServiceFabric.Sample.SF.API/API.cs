using System.Collections.ObjectModel;
using System.Fabric;
using System.Fabric.Description;
using System.Net;
using System.Security.Authentication;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Nandun.Reference.ServiceFabric.Sample.SF.API;

/// <summary>
/// The FabricRuntime creates an instance of this class for each service type instance.
/// </summary>
internal sealed class API : StatelessService
{
    public API(StatelessServiceContext context)
        : base(context)
    { }

    /// <summary>
    /// Optional override to create listeners (like tcp, http) for this service instance.
    /// </summary>
    /// <returns>The collection of listeners.</returns>
    protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
    {
        return new ServiceInstanceListener[]
        {
            new(serviceContext =>
                new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                {
                    ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                    return new WebHostBuilder()
                                .UseKestrel(opt =>
                                {
                                    int port = serviceContext.CodePackageActivationContext.GetEndpoint("ServiceEndpoint").Port;
                                    opt.Listen(IPAddress.IPv6Any, port, listenOptions =>
                                    {
                                        listenOptions.UseHttps(GetCertificateFromStore(serviceContext));
                                    });
                                })
                                .ConfigureServices(
                                    services => services
                                        .AddSingleton<StatelessServiceContext>(serviceContext))
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseStartup<Startup>()
                                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                .UseUrls(url)
                                .Build();

                }))
        };
    }

    private static HttpsConnectionAdapterOptions GetCertificateFromStore(ServiceContext context)
    {
        KeyedCollection<string, ConfigurationProperty> parameters = context.CodePackageActivationContext
            .GetConfigurationPackageObject("Config")
            .Settings
            .Sections["ReferenceService"]
            .Parameters;

        return new HttpsConnectionAdapterOptions
        {
            //ServerCertificate =  TODO: Get Cert
            SslProtocols = SslProtocols.Tls12
        };
    }
}
