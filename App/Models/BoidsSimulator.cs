using System.Numerics;

/// <summary>
/// Simulates flocking behavior based on the Boids algorithm.
/// This simulation models the natural flocking behavior of birds using simple rules like separation, alignment, and cohesion.
/// See <see cref="https://en.wikipedia.org/wiki/Boids"/> for more details.
/// </summary>
public class BoidsSimulator : IBoidsSimulator
{
    private IRenderer _renderer;
    private readonly ILogger<BoidsSimulator> _logger;
    private readonly Random _random = new Random();
    private readonly BoidsSimulatorOptions _options;
    private BirdOidObject[] _boids = Array.Empty<BirdOidObject>();

    public BoidsSimulator(
        IRenderer renderer,
        ILogger<BoidsSimulator> logger,
        BoidsSimulatorOptions options)
    {
        _renderer = renderer;
        _logger = logger;
        _options = options;
    }

    /// <summary>
    /// Sets up the initial state of the boids simulation.
    /// Initializes boids with random positions and velocities within the simulation bounds.
    /// </summary>
    public async Task SetupAsync()
    {
        await _renderer.SetupAsync(_options);

        var boids = new BirdOidObject[_options.Count];

        for (var index = 0; index < _options.Count; index++)
        {
            var positionX = (float)_random.NextDouble() * _options.Width;
            var positionY = (float)_random.NextDouble() * _options.Height;
            var position = new Vector2(positionX, positionY);
            
            var velocityX = (float)(_random.NextDouble() * 2 - 1) * _options.MaxSpeed;
            var velocityY = (float)(_random.NextDouble() * 2 - 1) * _options.MaxSpeed;
            var velocity = new Vector2(velocityX, velocityY);

            var birdOidObject = new BirdOidObject(index, position, velocity, _options);
            boids[index] = birdOidObject;
        }

        _boids = boids;
    }

    /// <summary>
    /// Updates the state of all boids based on the Boids rules and renders them on the canvas.
    /// </summary>
    /// <param name="isDebugEnabled">If true, enables debug rendering features such as visualizing boid vectors.</param>
    /// <param name="cancellationToken">Token used to cancel the update operation if needed.</param>
    public async Task UpdateAndRenderAsync(bool isDebugEnabled, CancellationToken cancellationToken)
    {
        foreach(var boid in _boids)
        {
            cancellationToken.ThrowIfCancellationRequested();
            boid.Update(_boids, _random);
        }

        cancellationToken.ThrowIfCancellationRequested();
        await _renderer.RenderAsync(_boids, _options, _random, isDebugEnabled);
    }

    /// <summary>
    /// Cleans up resources used by the simulation.
    /// Typically called when the simulation is stopped or reset.
    /// </summary>
    public async Task CleanupAsync()
    {
        await _renderer.CleanupAsync();
    }
}