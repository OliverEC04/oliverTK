using System;
using OpenTK.Graphics.OpenGL4;

namespace snake
{
    public class ElementBuffer : IDisposable
    {
        private readonly int _handle;
        
        public ElementBuffer(uint[] indices)
        {
            _handle = GL.GenBuffer();
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, 
                BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }
        
        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}