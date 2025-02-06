public interface ICanvas2DContext
{
    Task ClearRectAsync(double x, double y, double width, double height);
    Task SetFillStyleAsync(object value);
    Task BeginBatchAsync();
    Task ArcAsync(double x, double y, double radius, double startAngle, double endAngle, bool? anticlockwise = null);
    Task FillTextAsync(string text, double x, double y, double? maxWidth = null);
    Task FillAsync();
    Task BeginPathAsync();
    Task ClosePathAsync();
    Task EndBatchAsync();
}