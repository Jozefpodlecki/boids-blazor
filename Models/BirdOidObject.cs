using System.Numerics;
using System.Text;
using Microsoft.Extensions.Options;

public class BirdOidObject
{
    private int _index;
    private readonly BoidsSimulatorOptions _options;
    public Vector2 Velocity;
    public Vector2 Position;

    public BirdOidObject(int index, Vector2 position, Vector2 velocity, BoidsSimulatorOptions options)
    {
        _index = index;
        _options = options;
        Position = position;
        Velocity = velocity;
    }

    public void Update(IEnumerable<BirdOidObject> boids)
    {
        var alignment = Align(boids);
        var cohesion = Cohere(boids);
        var separation = Separate(boids);

        // Console.WriteLine(Velocity);
        Velocity += alignment + cohesion + separation;
        // Velocity *= _options.DampingFactor;
        // var steeringForce = (alignment + cohesion + separation) * 0.5;
        // Console.WriteLine("{0} {1} {2} {3}", Velocity, alignment, cohesion, separation);
        var min = -Vector2.One * _options.MaxSpeed;
        var max = Vector2.One * _options.MaxSpeed;
        Velocity = Vector2.Clamp(Velocity, min, max);
        // Console.WriteLine("{0} {1} {2}", Velocity, min, max);

        Position += Velocity;

        if (Position.X < 0) Position.X = _options.Width;
        if (Position.X > _options.Width) Position.X = 0;
        if (Position.Y < 0) Position.Y = _options.Height;
        if (Position.Y > _options.Height) Position.Y = 0;
    }

    private Vector2 Align(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var total = 0;

        foreach (var other in boids)
        {
            var distance = Vector2.Distance(Position, other.Position);

            if (other == this || distance > _options.PerceptionRadius) 
            {
                continue;
            }

            steering += other.Velocity;
            total++;
        }

        if (total > 0)
        {
            steering /= total;

            if (steering != Vector2.Zero)
            {
                steering = Vector2.Normalize(steering) * _options.MaxSpeed;
            }
                
            steering -= Velocity;
        }

        if(float.IsNaN(steering.X) || float.IsNaN(steering.Y)) {
            var dump = new StringBuilder();

            dump.AppendFormat("Total = {0}, Steering = {1}, MaxSpeed = {2}, PerceptionRadius = {3} \n", total, steering, _options.MaxSpeed, _options.PerceptionRadius);

            foreach (var boid in boids) {
                dump.AppendFormat("{0}\n", boid);
            }

            Console.WriteLine(dump.ToString());
        }

        return steering;
    }

    private Vector2 Cohere(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var total = 0;

        foreach (var other in boids)
        {
            var distance = Vector2.Distance(Position, other.Position);

            if (other == this || distance > _options.PerceptionRadius) {
                continue;
            }

            steering += other.Position;
            total++;
        }

        if (total > 0)
        {
            steering /= total;
            steering -= Position;
            steering = Vector2.Normalize(steering) * _options.MaxSpeed;
            steering -= Velocity;
        }

        return steering;
    }

    private Vector2 Separate(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var total = 0;

        foreach (var other in boids)
        {
            var distance = Vector2.Distance(Position, other.Position);

            if (other == this || distance > _options.SeparationDistance)
            {
                continue;
            }

            var diff = Position - other.Position;
            diff /= distance;
            steering += diff;
            total++;
        }

        if (total > 0)
        {
            steering /= total;
            steering = Vector2.Normalize(steering) * _options.MaxSpeed;
            steering -= Velocity;
        }

        return steering;
    }

    public override string ToString()
    {
        return $"Index = {_index}, Position = {Position}, Velocity = {Velocity}";
    }
}