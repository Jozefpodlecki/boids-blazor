
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverageAttribute]
internal class CanvasRenderer : IRenderer
{
    private readonly ICanvas2DContext _context;

    public CanvasRenderer(ICanvas2DContext context)
    {
        _context = context;
    }

    public Task SetupAsync(BoidsSimulatorOptions options) => Task.CompletedTask;

    public async Task RenderAsync(BirdOidObject[] boids, BoidsSimulatorOptions options, Random random, bool isDebugEnabled)
    {
        await _context.ClearRectAsync(0, 0, options.Width, options.Height);

        var radius = 3;
        var startAngle = 0;
        var endAngle = 2 * Math.PI;
        var fillColor = "#FFF";
        await _context.SetFillStyleAsync(fillColor);
        await _context.BeginBatchAsync();

        foreach (var boid in boids)
        {
            await _context.BeginPathAsync();
            await _context.ArcAsync(boid.Position.X, boid.Position.Y, radius, startAngle , endAngle);
            
            if(isDebugEnabled && boid.Index % 10 == 0)
            {
                await _context.FillTextAsync(boid.ToString(), boid.Position.X, boid.Position.Y);
            }

            await _context.FillAsync();
            await _context.ClosePathAsync();
        }

        await _context.EndBatchAsync();
    }

    public Task CleanupAsync() => Task.CompletedTask;
}