using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlockingSimulator;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverageAttribute]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddSingleton<IRendererFactory, RendererFactory>();
        builder.Services.AddSingleton<IBoidsSimulatorFactory, BoidsSimulatorFactory>();

        if (!builder.HostEnvironment.IsDevelopment())
        {
            builder.Logging.SetMinimumLevel(LogLevel.Information);
        }
        else
        {
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
        }

        await builder.Build().RunAsync();
    }
}