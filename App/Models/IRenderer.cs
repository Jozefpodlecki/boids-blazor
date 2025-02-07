public interface IRenderer
{
    Task SetupAsync(BoidsSimulatorOptions options);
    Task RenderAsync(BirdOidObject[] boids, BoidsSimulatorOptions options, Random random, bool isDebugEnabled);
    Task CleanupAsync();
}