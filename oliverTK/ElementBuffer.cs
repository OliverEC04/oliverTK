using System;
using OpenTK.Graphics.ES11;

namespace oliverTK
{
    public class ElementBuffer : IDisposable
    {
        private readonly int _handle;
        
        public ElementBuffer(uint[] indices)
        {
            _handle = GL.GenBuffer();
            Bind();
            GL.BufferData(BufferTargetArb.ElementArrayBuffer, indices.Length * sizeof(uint), indices, 
                BufferUsageArb.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTargetArb.ElementArrayBuffer, _handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTargetArb.ElementArrayBuffer, 0);
        }
        
        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}