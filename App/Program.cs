using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FlockingSimulator;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

if (!builder.HostEnvironment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Information);
}
else
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

await builder.Build().RunAsync();
