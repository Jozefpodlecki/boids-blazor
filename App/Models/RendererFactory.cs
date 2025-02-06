using Blazor.Extensions;

public static class RendererFactory
{
    public static async Task<IRenderer> GetRendererAsync(string context, BECanvasComponent component)
    {
        if (context == "webgl")
        {
            var webGLContext = await component.CreateWebGLAsync();
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
}