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

        public void SetUniform3(string name, Vector3 uniform)
        {
            Bind();
            int location = GL.GetUniformLocation(_handle, name);
            GL.Uniform3(location, uniform);
        }
        
        public void SetUniform1(string name, float uniform)
        {
            Bind();
            int location = GL.GetUniformLocation(_handle, name);
            GL.Uniform1(location, uniform);
        }

        public void SetUniform(string name, int uniform)
        {
            Bind();
            int location = GL.GetUniformLocation(_handle, name);
            GL.Uniform1(location, uniform);
        }

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