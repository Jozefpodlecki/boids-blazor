
using System.Diagnostics.CodeAnalysis;
using Blazor.Extensions.Canvas.WebGL;

[ExcludeFromCodeCoverageAttribute]
internal class WebGLRenderer : IRenderer
{
    private readonly IWebGLContext _context;
    private WebGLProgram _program;
    private WebGLBuffer _buffer;
    private float[] _boidVertices;

    public WebGLRenderer(IWebGLContext context)
    {
        _context = context;
    }

    public async Task SetupAsync(BoidsSimulatorOptions options)
    {
        var vertexShaderSource = @"
            attribute vec2 a_Position;
            void main() {
                gl_PointSize = 4.0;
                gl_Position = vec4(a_Position, 0.0, 1.0);
            }";

        var fragmentShaderSource = @"
            precision mediump float;
            void main() {
                gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
            }";

        var vertexShader = await _context.CreateShaderAsync(ShaderType.VERTEX_SHADER);
        await _context.ShaderSourceAsync(vertexShader, vertexShaderSource);
        await _context.CompileShaderAsync(vertexShader);

        var fragmentShader = await _context.CreateShaderAsync(ShaderType.FRAGMENT_SHADER);
        await _context.ShaderSourceAsync(fragmentShader, fragmentShaderSource);
        await _context.CompileShaderAsync(fragmentShader);

        _program = await _context.CreateProgramAsync();
        await _context.AttachShaderAsync(_program, vertexShader);
        await _context.AttachShaderAsync(_program, fragmentShader);
        await _context.LinkProgramAsync(_program);
        await _context.UseProgramAsync(_program);

        await _context.DeleteShaderAsync(vertexShader);
        await _context.DeleteShaderAsync(fragmentShader);

        _buffer = await _context.CreateBufferAsync();

        _boidVertices = new float[options.Count * 2];
    }

    public async Task RenderAsync(BirdOidObject[] boids, BoidsSimulatorOptions options, Random random, bool isDebugEnabled)
    {
        await _context.ClearColorAsync(0, 0, 0, 1);
        await _context.ClearAsync(BufferBits.COLOR_BUFFER_BIT);

        var boidsCount = boids.Count();

        for (int i = 0; i < boidsCount; i++)
        {
            var boid = boids[i];
            
            var x = boid.Position.X;
            var y = boid.Position.Y;
            _boidVertices[2 * i] = (x / options.Width) * 2.0f - 1.0f;
            _boidVertices[2 * i + 1] = (y / options.Height) * 2.0f - 1.0f;
        }

        await _context.BindBufferAsync(BufferType.ARRAY_BUFFER, _buffer);
        await _context.BufferDataAsync(BufferType.ARRAY_BUFFER, _boidVertices, BufferUsageHint.STATIC_DRAW);

        var positionLocation = await _context.GetAttribLocationAsync(_program, "a_Position");
        await _context.VertexAttribPointerAsync((uint)positionLocation, 2, DataType.FLOAT, false, 0, 0);
        await _context.EnableVertexAttribArrayAsync((uint)positionLocation);

        await _context.DrawArraysAsync(Primitive.POINTS, 0, boidsCount);

    }

    public async Task CleanupAsync()
    {
        try
        {
            await _context.DeleteBufferAsync(_buffer);
            await _context.DeleteProgramAsync(_program);
        }
        catch(Exception)
        {

        }
    }
}