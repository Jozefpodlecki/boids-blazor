using System.Numerics;
using System.Text;
using Microsoft.Extensions.Options;

public class BirdOidObject
{
    public int Index { get; }
    private readonly BoidsSimulatorOptions _options;
    public Vector2 Velocity;
    public Vector2 Position;
    public float Distance;

    public BirdOidObject(int index, Vector2 position, Vector2 velocity, BoidsSimulatorOptions options)
    {
        Index = index;
        _options = options;
        Position = position;
        Velocity = velocity;
    }

    public void Update(IEnumerable<BirdOidObject> boids, Random random)
    {
        ChangeDirectionRandomly(random);

        var boidsInPerception = GetBoidsInPerception(boids);
        Algorithm(boidsInPerception);
        LimitSpeed();

        HandleEdges();

        Position += Velocity;
    }

    public void ChangeDirectionRandomly(Random random)
    {
        if(random.NextDouble() <= 0.7)
        {
            return;
        }

        var newVelocityX = (float)(random.NextDouble() * 2 - 1) * _options.MaxSpeed;
        var newVelocityY = (float)(random.NextDouble() * 2 - 1) * _options.MaxSpeed;
        var newVelocity = new Vector2(newVelocityX, newVelocityY);

        Velocity += newVelocity * _options.DirectionChangeFactor;
    }

    public IEnumerable<BirdOidObject> GetBoidsInPerception(IEnumerable<BirdOidObject> boids)
    {    
        var inPerception = new List<BirdOidObject>();

        foreach (var other in boids)
        {
            var distance = Vector2.Distance(Position, other.Position);
            other.Distance = distance;

            if (other == this || distance > _options.PerceptionRadius) 
            {
                continue;
            }

            inPerception.Add(other);
        }

        return inPerception;
    }

    private void Algorithm(IEnumerable<BirdOidObject> boids) {
        var alignment = Align(boids);
        // var cohesion = COMCohesion(boids);
        var cohesion = WeightedCohesion(boids);
        // var separation = NormalizedWeightingSeparate(boids);
        var separation = ExponentialSeparate(boids);

        Velocity += alignment + cohesion + separation;
    }

    public void LimitSpeed()
    {
        // var min = -Vector2.One * _options.MaxSpeed;
        // var max = Vector2.One * _options.MaxSpeed;
        // Velocity = Vector2.Clamp(Velocity, min, max);

        var speed = Velocity.Length();

        if (speed > _options.MaxSpeed)
        {
            var factor = _options.MaxSpeed / speed;
            Velocity *= factor;
        }
    }

    public void HandleEdges() {
        // if (Position.X < 0) Position.X = _options.Width;
        // if (Position.X > _options.Width) Position.X = 0;
        // if (Position.Y < 0) Position.Y = _options.Height;
        // if (Position.Y > _options.Height) Position.Y = 0;

        var steer = Vector2.Zero;
        var combinedDistance = _options.AvoidWallsDistance;

        if (Position.X < combinedDistance)
        {
            steer.X += (combinedDistance - Position.X) / _options.AvoidWallsDistance;
        }
        else if (Position.X > _options.Width - combinedDistance)
        {
            steer.X -= (Position.X - (_options.Width - combinedDistance)) / _options.AvoidWallsDistance;
        }

        if (Position.Y < combinedDistance)
        {
            steer.Y += (combinedDistance - Position.Y) / _options.AvoidWallsDistance;
        }
        else if (Position.Y > _options.Height - combinedDistance)
        {
            steer.Y -= (Position.Y - (_options.Height - combinedDistance)) / _options.AvoidWallsDistance;
        }

        Velocity += steer;
    }

    private Vector2 Align(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var total = 0;

        foreach (var other in boids)
        {
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

        // if(float.IsNaN(steering.X) || float.IsNaN(steering.Y)) {
        //     var dump = new StringBuilder();

        //     dump.AppendFormat("Total = {0}, Steering = {1}, MaxSpeed = {2}, PerceptionRadius = {3} \n", total, steering, _options.MaxSpeed, _options.PerceptionRadius);

        //     foreach (var boid in boids) {
        //         dump.AppendFormat("{0}\n", boid);
        //     }

        //     Console.WriteLine(dump.ToString());
        // }

        return steering;
    }

    /// <summary>
    /// Computes the cohesion force for the current boid, guiding it toward the center of mass (COM) 
    /// of nearby boids to maintain flock unity.
    /// 
    /// Steps:
    /// 1. Detect neighboring boids within a perception radius.
    /// 2. Calculate the center of mass (COM) of the detected boids.
    /// 3. Compute the steering force required to move toward the COM.
    /// 4. Return the force to adjust the boid's velocity.
    /// </summary>
    /// <param name="boids">A collection of other boids in the environment with their positions and velocities.</param>
    /// <returns>A vector representing the cohesion steering force.</returns>
    private Vector2 COMCohesion(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var total = 0;

        foreach (var other in boids)
        {
            steering += other.Position;
            total++;
        }

        if (total > 0)
        {
            steering /= total;
            steering -= Position;
            
            if (steering != Vector2.Zero)
            {
                steering = Vector2.Normalize(steering) * _options.MaxSpeed;
            }

            steering -= Velocity;
        }

        return steering;
    }

    private Vector2 WeightedCohesion(IEnumerable<BirdOidObject> boids)
    {
        var steering = Vector2.Zero;
        var totalWeight = 0f;

        foreach (var other in boids)
        {
            var distance = other.Distance;

            if (distance <= 0 || other == this) 
            {
                continue;
            }

            var weight = 1 / distance;
            steering += other.Position * weight;
            totalWeight += weight;
        }

        if (totalWeight > 0)
        {
            steering /= totalWeight;
            steering -= Position;

            if (steering != Vector2.Zero)
            {
                steering = Vector2.Normalize(steering) * _options.MaxSpeed;
            }

            steering -= Velocity;
        }

        return steering;
    }

        private Vector2 NormalizedWeightingSeparate(IEnumerable<BirdOidObject> boids)
        {
            var steering = Vector2.Zero;
            var total = 0;

            foreach (var other in boids)
            {
                var diff = Position - other.Position;
                diff /= other.Distance;
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

    private Vector2 ExponentialSeparate(IEnumerable<BirdOidObject> boids)
    {
        var separationForce = Vector2.Zero;

        foreach (var other in boids)
        {
            var combinedSeparationRadius = _options.SeparationDistance * 2;

            if (other.Distance < combinedSeparationRadius)
            {
                var diff = Position - other.Position;
                var separationFactor = (float)Math.Exp(-(other.Distance - _options.SeparationDistance) / _options.SeparationDistance);

                separationForce += (diff / other.Distance) * separationFactor;
            }
        }

        return separationForce;
    }

    public override string ToString()
    {
        return $"Index = {Index}, Position = {Position}, Velocity = {Velocity}";
    }
}