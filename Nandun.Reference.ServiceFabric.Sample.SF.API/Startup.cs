using System.Fabric;
using Nandun.Reference.ServiceFabric.Sample.DependencyInjection;

namespace Nandun.Reference.ServiceFabric.Sample.SF.API;

/// <summary>
/// Runtime creates an instance of this class.
/// </summary>
public class Startup
{
    /// <summary>
    /// This is initiated by the runtime.
    /// </summary>
    /// <param name="context"></param>
    public Startup(StatelessServiceContext context)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddServiceFabricConfiguration(context) // Add Service Fabric configuration settings.
            .AddEnvironmentVariables();

        Configuration = builder.Build();
    }

    /// <summary>
    /// Represents a set of key/value application configuration properties.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApi();

        services.AddControllers();

    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
