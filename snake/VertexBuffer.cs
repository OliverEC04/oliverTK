using System;
using OpenTK.Graphics.OpenGL;

namespace snake
{
    public class VertexBuffer : IDisposable
    {
        private uint _handle;

        public VertexBuffer(float[] vertices)
        {
            _handle = GL.GenBuffer();
            Bind();
            GL.BufferData(BufferTargetARB.ArrayBuffer, vertices, BufferUsageARB.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTargetARB.ArrayBuffer, _handle);
        }

        public void Dispose()
        {
            GL.DeleteBuffers(1, _handle);
        }
    }
}