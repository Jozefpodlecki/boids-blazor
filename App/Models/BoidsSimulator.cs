using System.Numerics;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions.Canvas.WebGL;

/// <summary>
/// See <see cref="https://en.wikipedia.org/wiki/Boids"/>
/// 
/// </summary>
public class BoidsSimulator
{
    private IRenderer _renderer;
    private readonly ILogger _logger;
    private readonly Random _random = new Random();
    private readonly BoidsSimulatorOptions _options;
    private readonly BirdOidObject[] _boids = Array.Empty<BirdOidObject>();

    public BoidsSimulator(
        IRenderer renderer,
        ILogger logger,
        BoidsSimulatorOptions options)
    {
        _renderer = renderer;
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

    public async Task SetupAsync()
    {
        await _renderer.SetupAsync();
    }

    public async Task UpdateAndRenderAsync(bool isDebugEnabled)
    {
        await _renderer.RenderAsync(_boids, _options, _random, isDebugEnabled);
    }

    public async Task CleanupAsync()
    {
        await _renderer.CleanupAsync();
    }
}