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
    private readonly IList<BirdOidObject> _boids = new List<BirdOidObject>();

    public BoidsSimulator(ILogger logger, BoidsSimulatorOptions options)
    {
        _logger = logger;
        _options = options;

        for (var index = 0; index < options.Count; index++)
        {
            var positionX = (float)_random.NextDouble() * options.Width;
            var positionY = (float)_random.NextDouble() * options.Height;
            var position = new Vector2(positionX, positionY);
            
            var velocityX = (float)(_random.NextDouble() * 2 - 1);
            var velocityY = (float)(_random.NextDouble() * 2 - 1);
            var velocity = new Vector2(velocityX, velocityY);

            velocity = Vector2.Normalize(velocity) * 2f;

            var birdOidObject = new BirdOidObject(index, position, velocity, options);
            _boids.Add(birdOidObject);
        }
    }

    public async Task UpdateAndRenderAsync(Canvas2DContext context)
    {
        var radius = 3;
        var startAngle = 0;
        var endAngle = 2 * Math.PI;
        var fillColor = "#FFF";

        foreach (var boid in _boids)
        {
            boid.Update(_boids);
            await context.BeginPathAsync();
            await context.ArcAsync(boid.Position.X, boid.Position.Y, radius, startAngle , endAngle);
            await context.SetFillStyleAsync(fillColor);
            await context.FillAsync();
            await context.ClosePathAsync();
        }
       
    }
}