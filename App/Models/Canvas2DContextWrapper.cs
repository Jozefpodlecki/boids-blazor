using Blazor.Extensions.Canvas.Canvas2D;

public class Canvas2DContextWrapper : ICanvas2DContext
{
    private readonly Canvas2DContext _context;

    public Canvas2DContextWrapper(Canvas2DContext context)
    {
        _context = context;
    }

    public Task ClearRectAsync(double x, double y, double width, double height) => _context.ClearRectAsync(x, y, width, height);

    public Task SetFillStyleAsync(object value) => _context.SetFillStyleAsync(value);

    public Task BeginBatchAsync() => _context.BeginBatchAsync();

    public Task ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise) => _context.ArcAsync(x, y, radius, startAngle, endAngle, anticlockwise);

    public Task FillTextAsync(string text, double x, double y, double? maxWidth) => _context.FillTextAsync(text, x, y, maxWidth);

    public Task FillAsync() => _context.FillAsync();

    public Task BeginPathAsync() => _context.BeginPathAsync();

    public Task ClosePathAsync() => _context.ClosePathAsync();

    public Task EndBatchAsync() => _context.EndBatchAsync();
}