using System.Diagnostics;
using System.Fabric;
using Nandun.Reference.ServiceFabric.Sample.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Runtime;

namespace Nandun.Reference.ServiceFabric.Sample.SF.Processor;

/// <summary>
/// An instance of this class is created for each service instance by the Service Fabric runtime.
/// </summary>
internal sealed class Processor : StatelessService
{
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes an instance of <see cref="Process"/> class with specific configurations.
    /// </summary>
    /// <param name="context">The service fabric context.</param>
    public Processor(StatelessServiceContext context) : base(context)
    {
        Configuration = context.Configure(new ConfigurationBuilder());
    }

    /// <inheritdoc cref="StatelessService.RunAsync(CancellationToken)"/>
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
        IServiceCollection ServiceCollection = new ServiceCollection();

        IServiceProvider serviceProvider = ConfigureServices(Configuration, ServiceCollection).BuildServiceProvider();
        try
        {
            ISampleProcessor processor = serviceProvider.GetRequiredService<ISampleProcessor>();

            Stopwatch refreshStopWatch = Stopwatch.StartNew();
            while (!cancellationToken.IsCancellationRequested)
            {
                await processor.ProcessAsync(cancellationToken);
                if (refreshStopWatch.Elapsed > TimeSpan.FromHours(1))
                {
                    processor = serviceProvider.GetRequiredService<ISampleProcessor>();
                    refreshStopWatch.Restart();
                }

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                cancellationToken.ThrowIfCancellationRequested();
            }
        }
        catch (Exception exception)
        {
            // Log and throw
            throw;
        }
    }

    private IServiceCollection ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddProcessor();

        return services;
    }
}
