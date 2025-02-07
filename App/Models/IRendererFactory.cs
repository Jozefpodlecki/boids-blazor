using Blazor.Extensions;

public interface IRendererFactory
{
    Task<IRenderer> CreateAsync(string context, BECanvasComponent component);
}