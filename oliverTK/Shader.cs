using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace oliverTK
{
    public class Shader : IDisposable
    {
        private readonly int _handle;
        
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