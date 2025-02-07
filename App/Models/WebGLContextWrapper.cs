using System.Diagnostics.CodeAnalysis;
using Blazor.Extensions.Canvas.WebGL;

[ExcludeFromCodeCoverageAttribute]
public class WebGLContextWrapper : IWebGLContext
{
    private readonly WebGLContext _context;

    public WebGLContextWrapper(WebGLContext context)
    {
        _context = context;
    }

    public Task AttachShaderAsync(WebGLProgram program, WebGLShader shader) => _context.AttachShaderAsync(program, shader);

    public Task BindBufferAsync(BufferType target, WebGLBuffer buffer) => _context.BindBufferAsync(target, buffer);

    public Task BufferDataAsync<T>(BufferType target, T[] data, BufferUsageHint usage) => _context.BufferDataAsync(target, data, usage);

    public Task ClearAsync(BufferBits mask) => _context.ClearAsync(mask);

    public Task ClearColorAsync(float red, float green, float blue, float alpha) => _context.ClearColorAsync(red, green, blue, alpha);

    public Task CompileShaderAsync(WebGLShader shader) => _context.CompileShaderAsync(shader);

    public Task<WebGLBuffer> CreateBufferAsync() => _context.CreateBufferAsync();

    public Task<WebGLProgram> CreateProgramAsync() => _context.CreateProgramAsync();

    public Task<WebGLShader> CreateShaderAsync(ShaderType type) => _context.CreateShaderAsync(type);

    public Task DeleteBufferAsync(WebGLBuffer buffer) => _context.DeleteBufferAsync(buffer);

    public Task DeleteProgramAsync(WebGLProgram program) => _context.DeleteProgramAsync(program);

    public Task DeleteShaderAsync(WebGLShader shader) => _context.DeleteShaderAsync(shader);

    public Task DrawArraysAsync(Primitive mode, int first, int count) => _context.DrawArraysAsync(mode, first, count);

    public Task EnableVertexAttribArrayAsync(uint index) => _context.EnableVertexAttribArrayAsync(index);

    public Task LinkProgramAsync(WebGLProgram program) => _context.LinkProgramAsync(program);

    public Task ShaderSourceAsync(WebGLShader shader, string source) => _context.ShaderSourceAsync(shader, source);

    public Task UseProgramAsync(WebGLProgram program) => _context.UseProgramAsync(program);

    public Task VertexAttribAsync(uint index, params float[] value) => _context.VertexAttribAsync(index, value);

    public Task<int> GetAttribLocationAsync(WebGLProgram program, string name) => _context.GetAttribLocationAsync(program, name);

    public Task VertexAttribPointerAsync(uint index, int size, DataType type, bool normalized, int stride, long offset)
        => _context.VertexAttribPointerAsync(index, size, type, normalized, stride, offset);
}