namespace Nandun.Reference.ServiceFabric.Sample;

/// <summary>
/// Order data processor to populate database
/// </summary>
public interface ISampleProcessor
{
    /// <summary>
    /// Overriding ProcessTransactionAsync method in order to add required telemetry around it
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task ProcessAsync(CancellationToken cancellationToken);
}


internal class SampleProcessor : ISampleProcessor
{

    /// <summary>
    /// Ctor
    /// </summary>

    public SampleProcessor()
    {
    }

    public async Task ProcessAsync(CancellationToken cancellationToken)
    {
        // Do Stuff
    }

}
