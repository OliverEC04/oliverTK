using System;
using OpenTK.Graphics.OpenGL;

namespace snake
{
    public class ElementBuffer : IDisposable
    {
        private readonly uint _handle;
        
        public ElementBuffer(uint[] indices)
        {
            _handle = GL.GenBuffers();
            Bind();
            GL.BufferData(BufferTargetARB.ElementArrayBuffer, indices, 
                BufferUsageARB.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, _handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }
        
        public unsafe void Dispose()
        {
            GL.DeleteBuffers(1, (uint*) _handle);
        }
    }
}