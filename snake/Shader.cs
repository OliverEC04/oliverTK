using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace snake
{
    public class Shader : IDisposable
    {
        private readonly int _handle;
        private readonly int _vColorLocation;
        
        public Shader(string vertexPath, string fragmentPath)
        {
            _handle = GL.CreateProgram();

            int vertexShader = CreateShader(ShaderType.VertexShader, vertexPath);
            int fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentPath);
            
            GL.LinkProgram(_handle);

            string infoLog = GL.GetProgramInfoLog(_handle);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
            
            DeleteShader(vertexShader);
            DeleteShader(fragmentShader);
        }

        private int CreateShader(ShaderType shaderType, string path)
        {
            string src = File.ReadAllText(path);
            int shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);

            string infoLog = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(infoLog))
            {
                throw new Exception(infoLog);
            }
            
            GL.AttachShader(_handle, shader);

            return shader;
        }

        public int GetAttributeLocation(string attributeName)
        {
            return GL.GetAttribLocation(_handle, attributeName);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(_handle, name);
        }

        public void SetUniform(int location, int value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref int value) =>
            GL.ProgramUniform1(_handle, location, 1, ref value);
        
        public void SetUniform(int location, float value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref float value) =>
            GL.ProgramUniform1(_handle, location, 1, ref value);
        
        public void SetUniform(int location, double value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref double value) =>
            GL.ProgramUniform1(_handle, location, 1, ref value);
        
        public void SetUniform(int location, Vector2 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector2 value) =>
            GL.ProgramUniform2(_handle, location, 1, ref value.X);
        
        public void SetUniform(int location, Vector3 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector3 value) =>
            GL.ProgramUniform3(_handle, location, 1, ref value.X);
        
        public void SetUniform(int location, Vector4 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector4 value) =>
            GL.ProgramUniform4(_handle, location, 1, ref value.X);
        
        
        public void SetUniform(int location, Matrix4 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Matrix4 value) =>
            GL.ProgramUniformMatrix4(_handle, location, 1, false, ref value.Row0.X);

        private void DeleteShader(int shader)
        {
            GL.DetachShader(_handle, shader);
            GL.DeleteShader(shader);
        }

        public void Bind()
        {
            GL.UseProgram(_handle);
        }

        public void Dispose()
        {
            GL.DeleteProgram(_handle);
        }
    }
}