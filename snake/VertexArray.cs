using System;
using OpenTK.Graphics.OpenGL4;

namespace snake
{
    public class VertexArray : IDisposable
    {
        private readonly int _handle;
        
        public VertexArray()
        {
            _handle = GL.GenVertexArray();
        }

        public void Bind()
        {
            GL.BindVertexArray(_handle);
        }

        public void SetEbo(ElementBuffer ebo)
        {
            Bind();
            ebo.Bind();
        }

        public void SetVertexAttribute(
            VertexBuffer vertexBuffer,
            int location,
            int count,
            int vertexSize,
            int offset)
        {
            Bind();
            vertexBuffer.Bind();
            
            GL.EnableVertexAttribArray(location);
            GL.VertexAttribPointer(location,
                count, 
                VertexAttribPointerType.Float,
                false,
                vertexSize * sizeof(float),
                offset * sizeof(float));
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(_handle);
        }
    }
}