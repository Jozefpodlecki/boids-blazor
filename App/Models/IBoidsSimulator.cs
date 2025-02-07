public interface IBoidsSimulator
{
    Task SetupAsync();
    Task UpdateAndRenderAsync(bool isDebugEnabled, CancellationToken token);
    Task CleanupAsync();
}