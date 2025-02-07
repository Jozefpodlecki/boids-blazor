using System;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class BoidsSimulatorTests
{
    private readonly Mock<IRenderer> _rendererMock;
    private readonly Mock<ILogger<BoidsSimulator>> _loggerMock;
    private readonly BoidsSimulatorOptions _options;
    private readonly BoidsSimulator _simulator;

    public BoidsSimulatorTests()
    {
        _rendererMock = new Mock<IRenderer>();
        _loggerMock = new Mock<ILogger<BoidsSimulator>>();
        _options = new BoidsSimulatorOptions
        {
            Width = 800,
            Height = 600,
            Count = 5,
            MaxSpeed = 2.0f
        };

        _simulator = new BoidsSimulator(_rendererMock.Object, _loggerMock.Object, _options);
    }

    [Fact]
    public async Task UpdateAndRenderAsync_ShouldCallRenderAsyncOnRenderer()
    {
        await _simulator.SetupAsync();
        var cancellationToken = CancellationToken.None;

        await _simulator.UpdateAndRenderAsync(false, cancellationToken);

        _rendererMock.Verify(r => r.RenderAsync(It.IsAny<BirdOidObject[]>(), _options, It.IsAny<Random>(), false), Times.Once);
    }

    [Fact]
    public async Task CleanupAsync_ShouldCallCleanupOnRenderer()
    {
        await _simulator.CleanupAsync();

        _rendererMock.Verify(r => r.CleanupAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAndRenderAsync_ShouldCancelIfTokenIsTriggered()
    {
        await _simulator.SetupAsync();
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await _simulator.UpdateAndRenderAsync(false, cts.Token);
        });
    }
}