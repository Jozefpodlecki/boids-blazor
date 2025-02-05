using System.Numerics;
using Blazor.Extensions.Canvas.Canvas2D;

/// <summary>
/// See <see cref="https://en.wikipedia.org/wiki/Boids"/>
/// 
/// </summary>
public class BoidsSimulator
{
    private readonly ILogger _logger;
    private readonly Random _random = new Random();
    private readonly BoidsSimulatorOptions _options;
    private readonly IEnumerable<BirdOidObject> _boids = Enumerable.Empty<BirdOidObject>();

    public BoidsSimulator(ILogger logger, BoidsSimulatorOptions options)
    {
        _logger = logger;
        _options = options;

        var boids = new BirdOidObject[options.Count];

        for (var index = 0; index < options.Count; index++)
        {
            var positionX = (float)_random.NextDouble() * options.Width;
            var positionY = (float)_random.NextDouble() * options.Height;
            var position = new Vector2(positionX, positionY);
            
            var velocityX = (float)(_random.NextDouble() * 2 - 1) * _options.MaxSpeed;
            var velocityY = (float)(_random.NextDouble() * 2 - 1) * _options.MaxSpeed;
            var velocity = new Vector2(velocityX, velocityY);

            var birdOidObject = new BirdOidObject(index, position, velocity, options);
            boids[index] = birdOidObject;
        }

        _boids = boids;
    }

    public async Task UpdateAndRenderAsync(Canvas2DContext context, bool isDebugEnabled)
    {
        var radius = 3;
        var startAngle = 0;
        var endAngle = 2 * Math.PI;
        var fillColor = "#FFF";
        await context.SetFillStyleAsync(fillColor);
        await context.BeginBatchAsync();

        foreach (var boid in _boids)
        {
            boid.Update(_boids, _random);
            await context.BeginPathAsync();
            await context.ArcAsync(boid.Position.X, boid.Position.Y, radius, startAngle , endAngle);
            
            if(isDebugEnabled && boid.Index % 10 == 0)
            {
                await context.FillTextAsync(boid.ToString(), boid.Position.X, boid.Position.Y);
            }

            await context.FillAsync();
            await context.ClosePathAsync();
        }

        await context.EndBatchAsync();
    }
}