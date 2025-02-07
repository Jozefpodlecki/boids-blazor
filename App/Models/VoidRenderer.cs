
public class VoidRenderer : IRenderer
{
    public VoidRenderer()
    {
    }

    public Task SetupAsync(BoidsSimulatorOptions options) => Task.CompletedTask;

    public async Task RenderAsync(BirdOidObject[] boids, BoidsSimulatorOptions options, Random random, bool isDebugEnabled)
    {
        
    }

    public Task CleanupAsync() => Task.CompletedTask;
}