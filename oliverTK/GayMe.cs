using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace oliverTK
{
    public class GayMe : GameWindow 
    {
        private int _vbo;
        private int _vao;
        private Shader _shader;

        public GayMe(int width, int height, string title)
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(1.0f, 0.3f, 0.3f, 1.0f);
            
            float[] vertices = new []
            {
                 -0.5f,  -0.5f,  0.0f,
                 -0.5f,   0.5f,  0.0f,
                  0.5f,  -0.5f,  0.0f,
            };

            _vao = GL.GenVertexArray();
            GL.BindVertexArray(_vao);
            
            _vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
                BufferUsageHint.StaticDraw);
            
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false,
                3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader = new Shader("shader.vert", "shader.frag");
            
            Size = new Vector2i(2000, 1000);
        }
        
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            KeyboardState key = KeyboardState;

            if (key.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            Render();
        }

        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _shader.Bind();
            GL.BindVertexArray(_vao);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            Render();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(_vbo);
            _shader.Dispose();
        }
    }
}