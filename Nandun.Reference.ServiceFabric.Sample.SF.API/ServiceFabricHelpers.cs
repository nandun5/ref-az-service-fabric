using System.Collections.ObjectModel;
using System.Fabric;
using System.Fabric.Description;

namespace Nandun.Reference.ServiceFabric.Sample.SF.API;

public class ServiceFabricConfigurationSource : IConfigurationSource
{
    private readonly ServiceContext serviceContext;

    public ServiceFabricConfigurationSource(ServiceContext serviceContext) => this.serviceContext = serviceContext;

    public IConfigurationProvider Build(IConfigurationBuilder builder) => (IConfigurationProvider)new ServiceFabricConfigurationProvider(this.serviceContext);
}

public class ServiceFabricConfigurationProvider : ConfigurationProvider
{
    private readonly ServiceContext serviceContext;

    public ServiceFabricConfigurationProvider(ServiceContext serviceContext) => this.serviceContext = serviceContext;

    public override void Load()
    {
        foreach (System.Fabric.Description.ConfigurationSection section in (Collection<System.Fabric.Description.ConfigurationSection>)this.serviceContext.CodePackageActivationContext.GetConfigurationPackageObject("Config").Settings.Sections)
        {
            foreach (ConfigurationProperty parameter in (Collection<ConfigurationProperty>)section.Parameters)
                this.Data[string.Format("{0}{1}{2}", (object)section.Name, (object)ConfigurationPath.KeyDelimiter, (object)parameter.Name)] = parameter.Value;
        }
    }
}
