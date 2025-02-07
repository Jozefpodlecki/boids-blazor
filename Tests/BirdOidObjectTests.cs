using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Moq;
using Xunit;

public class BirdOidObjectTests
{
    private readonly BoidsSimulatorOptions _options;

    public BirdOidObjectTests()
    {
        _options = new BoidsSimulatorOptions
        {
            Width = 800,
            Height = 600,
            MaxSpeed = 2.0f,
            PerceptionRadius = 100f,
            AvoidWallsDistance = 50,
            SeparationDistance = 20f,
            DirectionChangeFactor = 0.1f
        };
    }

    [Fact]
    public void Update_ShouldChangeDirectionRandomly()
    {
        var randomMock = new Mock<Random>();
        randomMock.Setup(r => r.NextDouble()).Returns(0.8); // Force direction change

        var boid = new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options);

        boid.ChangeDirectionRandomly(randomMock.Object);

        Assert.NotEqual(new Vector2(1, 1), boid.Velocity);
    }

    [Fact]
    public void GetBoidsInPerception_ShouldReturnCorrectBoidsWithinPerceptionRadius()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(50, 50), new Vector2(1, 1), _options),
            new BirdOidObject(2, new Vector2(200, 200), new Vector2(1, 1), _options),
        };

        var boid = boids[0];

        var boidsInPerception = boid.GetBoidsInPerception(boids);

        Assert.Single(boidsInPerception);
    }

    [Fact]
    public void Algorithm_ShouldUpdateVelocityBasedOnSurroundingBoids()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(50, 50), new Vector2(1, 1), _options),
            new BirdOidObject(2, new Vector2(100, 100), new Vector2(1, 1), _options),
        };

        var boid = boids[0];

        var initialVelocity = boid.Velocity;

        boid.Update(boids, new Random());

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void LimitSpeed_ShouldLimitVelocityToMaxSpeed()
    {
        var boid = new BirdOidObject(0, new Vector2(0, 0), new Vector2(3, 3), _options);

        boid.LimitSpeed();

        Assert.InRange(boid.Velocity.Length(), 0, _options.MaxSpeed);
    }

    [Fact]
    public void HandleEdges_ShouldSteerBoidAwayFromWalls()
    {
        var boid = new BirdOidObject(0, new Vector2(10, 10), new Vector2(0, 0), _options);

        var initialVelocity = boid.Velocity;

        boid.HandleEdges();

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void Align_ShouldAdjustVelocityBasedOnNeighborBoids()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(50, 50), new Vector2(1, 1), _options),
        };

        var boid = boids[0];
        var initialVelocity = boid.Velocity;

        boid.Update(boids, new Random());

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void COMCohesion_ShouldAdjustVelocityToCenterOfMass()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(50, 50), new Vector2(1, 1), _options),
        };

        var boid = boids[0];

        var initialVelocity = boid.Velocity;

        boid.Update(boids, new Random());

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void WeightedCohesion_ShouldApplyDistanceBasedWeighting()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(50, 50), new Vector2(1, 1), _options),
        };

        var boid = boids[0];

        var initialVelocity = boid.Velocity;

        boid.Update(boids, new Random());

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void ExponentialSeparate_ShouldApplyExponentialSeparationForce()
    {
        var boids = new List<BirdOidObject>
        {
            new BirdOidObject(0, new Vector2(0, 0), new Vector2(1, 1), _options),
            new BirdOidObject(1, new Vector2(10, 10), new Vector2(1, 1), _options),
        };

        var boid = boids[0];

        var initialVelocity = boid.Velocity;

        boid.Update(boids, new Random());

        Assert.NotEqual(initialVelocity, boid.Velocity);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectStringRepresentation()
    {
        var boid = new BirdOidObject(0, new Vector2(10, 20), new Vector2(1, 1), _options);

        var result = boid.ToString();

        Assert.Contains("Index = 0", result);
        Assert.Contains("Position = ", result);
        Assert.Contains("Velocity = ", result);
    }
}