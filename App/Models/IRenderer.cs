public interface IRenderer
{
    Task SetupAsync();
    Task RenderAsync(BirdOidObject[] boids, BoidsSimulatorOptions options, Random random, bool isDebugEnabled);
    Task CleanupAsync();
}