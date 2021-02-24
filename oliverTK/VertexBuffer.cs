using System;
using OpenTK.Graphics.OpenGL4;

namespace oliverTK
{
    public class VertexBuffer : IDisposable
    {
        private readonly int _handle;

        public VertexBuffer(float[] vertices)
        {
            _handle = GL.GenBuffer();
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
                BufferUsageHint.StaticDraw);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_handle);
        }
    }
}