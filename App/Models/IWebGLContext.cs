using Blazor.Extensions.Canvas.WebGL;

public interface IWebGLContext
{
    Task ClearAsync(BufferBits mask);
    Task ClearColorAsync(float red, float green, float blue, float alpha);
    Task BindBufferAsync(BufferType target, WebGLBuffer buffer);
    Task BufferDataAsync<T>(BufferType target, T[] data, BufferUsageHint usage);
    Task VertexAttribPointerAsync(uint index, int size, DataType type, bool normalized, int stride, long offset);
    Task EnableVertexAttribArrayAsync(uint index);
    Task<int> GetAttribLocationAsync(WebGLProgram program, string name);
    Task VertexAttribAsync(uint index, params float[] value);
    Task DrawArraysAsync(Primitive mode, int first, int count);
    Task<WebGLBuffer> CreateBufferAsync();
    Task DeleteBufferAsync(WebGLBuffer buffer);
    Task CompileShaderAsync(WebGLShader shader);
    Task<WebGLShader> CreateShaderAsync(ShaderType type);
    Task ShaderSourceAsync(WebGLShader shader, string source);
    Task<WebGLProgram> CreateProgramAsync();
    Task AttachShaderAsync(WebGLProgram program, WebGLShader shader);
    Task LinkProgramAsync(WebGLProgram program);
    Task UseProgramAsync(WebGLProgram program);
    Task DeleteShaderAsync(WebGLShader shader);
    Task DeleteProgramAsync(WebGLProgram program);
}