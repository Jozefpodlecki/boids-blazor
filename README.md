![Blazor Version](https://img.shields.io/badge/Blazor-9.0.1-blue.svg)
![.NET Framework Version](https://img.shields.io/badge/.net-9.0-blue.svg)
[![codecov](https://codecov.io/github/Jozefpodlecki/boids-blazor/graph/badge.svg?token=L79FIQ1RL7)](https://codecov.io/github/Jozefpodlecki/boids-blazor)

# Flocking Simulator 

The Flocking Simulator is a dynamic, interactive simulation based on the Boids Algorithm to simulate the behavior of a flock of birds or other entities. The algorithm allows a group of entities to exhibit collective behavior, such as cohesion, separation, and alignment, which results in the appearance of a natural flocking pattern.

The simulation allows users to configure various parameters, including the number of boids (agents), speed, separation distance, perception radius, and other simulation characteristics. The simulation is rendered on a canvas and can be controlled through a simple user interface.

## Core of the Application

### Blazor Framework:

The simulator is built using Blazor WebAssembly, leveraging Blazor Components for rendering the UI and interacting with JavaScript for animation and canvas handling.

### Canvas Rendering

The boid movements and behaviors are visualized on an HTML5 `<canvas>` element, where we use Canvas2DContext for rendering the positions and velocities of each boid.

### Simulator Configuration

The simulator is highly configurable, allowing users to modify parameters dynamically through the interface. The simulation can be started, paused, and reset, and real-time debugging information can be shown.

## Getting Started

### Tailwind

```
npx @tailwindcss/cli -i ./wwwroot/css/styles.css -o ./wwwroot/css/output.css --watch
```

```bash
git clone https://github.com/Jozefpodlecki/boids-blazor
cd boids-blazor
dotnet restore
dotnet run
```

## Tests

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Deploy

```bash
dotnet publish --configuration Release --output build
```

## Known issues

There's a bug where `BECanvasComponent` does not properly rerender when canvas context changes.
Workaround, resize browser window manually.

## Credits

- [VS Code and Blazor WASM: Debug with Hot Reload](https://dev.to/sacantrell/vs-code-and-blazor-wasm-debug-with-hot-reload-5317)
- [David Guida - Blazor GameDev](https://github.com/mizrael/BlazorCanvas)
- [Tim Deschryver - Integrating Tailwind CSS in Blazor](https://timdeschryver.dev/blog/integrating-tailwind-css-in-blazor)
- [Tabler Icons](https://tabler.io/icons)
- [Bird icons created by Artifex - Flaticon](https://www.flaticon.com/free-icons/bird)