using System.Diagnostics.CodeAnalysis;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.WebGL;

[ExcludeFromCodeCoverageAttribute]
internal class RendererFactory : IRendererFactory
{
    private readonly ILogger<IRendererFactory> _logger;

    public RendererFactory(ILogger<IRendererFactory> logger)
    {
        _logger = logger;
    }

    public async Task<IRenderer> CreateAsync(string context, BECanvasComponent component)
    {
        try
        {
            if (context == "webgl")
            {
                var webGLContextAttributes = new WebGLContextAttributes
                {
                    PreserveDrawingBuffer = true
                };
                var webGLContext = await component.CreateWebGLAsync(webGLContextAttributes);
                var canvasContext = new WebGLContextWrapper(webGLContext);
                return new WebGLRenderer(canvasContext);
            }
            else if (context == "2d")
            {
                var canvasContext = new Canvas2DContextWrapper(await component.CreateCanvas2DAsync());
                return new CanvasRenderer(canvasContext);
            }
            else
            {
                throw new InvalidOperationException("Unsupported context");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst resolving canvas context");
            return new VoidRenderer();
        }
        
    }
}