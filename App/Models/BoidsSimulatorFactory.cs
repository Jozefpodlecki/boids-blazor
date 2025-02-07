using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverageAttribute]
public class BoidsSimulatorFactory : IBoidsSimulatorFactory
{
    private readonly IServiceProvider _serviceProvider;

    public BoidsSimulatorFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IBoidsSimulator Create(IRenderer renderer, BoidsSimulatorOptions options)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<BoidsSimulator>>();
        return new BoidsSimulator(renderer, logger, options);
    }
}