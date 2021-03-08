using System;
using OpenTK.Graphics.OpenGL;

namespace snake
{
    public class VertexArray : IDisposable
    {
        private uint _handle;
        
        public VertexArray()
        {
            _handle = GL.GenVertexArrays();
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

        public unsafe void SetVertexAttribute(
            VertexBuffer vertexBuffer,
            uint location,
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
                0,
                vertexSize * sizeof(float),
                (void *) (offset * sizeof(float)));
        }

        public void Dispose()
        {
            GL.DeleteVertexArrays(1, ref _handle);
        }
    }
}