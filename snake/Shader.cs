using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace snake
{
    public class Shader : IDisposable
    {
        private uint _handle;
        private readonly int _vColorLocation;
        
        public Shader(string vertexPath, string fragmentPath)
        {
            _handle = GL.CreateProgram();

            uint vertexShader = CreateShader(ShaderType.VertexShader, vertexPath);
            uint fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentPath);
            
            GL.LinkProgram(_handle);

            int infoLogLength = 0;
            GL.GetProgramiv(_handle, ProgramPropertyARB.InfoLogLength, ref infoLogLength);
            if (infoLogLength != 0)
            {
                int tmp = 0;
                string infoLog = GL.GetProgramInfoLog(_handle, infoLogLength, ref tmp);
                throw new Exception(infoLog);
            }
            
            DeleteShader(vertexShader);
            DeleteShader(fragmentShader);
        }

        private uint CreateShader(ShaderType shaderType, string path)
        {
            string src = File.ReadAllText(path);
            uint shader = GL.CreateShader(shaderType);
            GL.ShaderSource(shader, src);
            GL.CompileShader(shader);

            int infoLogLength = 0;
            GL.GetProgramiv(shader, ProgramPropertyARB.InfoLogLength, ref infoLogLength);
            if (infoLogLength != 0)
            {
                int tmp = 0;
                string infoLog = GL.GetProgramInfoLog(shader, infoLogLength, ref tmp);
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
            GL.ProgramUniform1i(_handle, location, value);
        
        public void SetUniform(int location, float value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref float value) =>
            GL.ProgramUniform1f(_handle, location, value);
        
        public void SetUniform(int location, double value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref double value) =>
            GL.ProgramUniform1d(_handle, location, value);
        
        public void SetUniform(int location, Vector2 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector2 value) =>
            GL.ProgramUniform2fv(_handle, location, 1, value.X);
        
        public void SetUniform(int location, Vector3 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector3 value) =>
            GL.ProgramUniform3fv(_handle, location, 1, value.X);
        
        public void SetUniform(int location, Vector4 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Vector4 value) =>
            GL.ProgramUniform4fv(_handle, location, 1, value.X);
        
        
        public void SetUniform(int location, Matrix4 value) =>
            SetUniform(location, ref value);
        public void SetUniform(int location, ref Matrix4 value) =>
            GL.ProgramUniformMatrix4fv(_handle, location, 1, false, value.Row0.X);

        private void DeleteShader(uint shader)
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