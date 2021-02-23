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
        private readonly float[] _vertices = new []
        {
            -0.5f, -0.5f,  0.0f,
            -0.5f,  0.5f,  0.0f,
            0.5f,  0.5f,  0.0f,
            0.5f,  0.5f,  0.0f,
            0.5f, -0.5f,  0.0f,
            -0.5f, -0.5f,  0.0f,
        };

        private VertexBuffer _vbo;
        private VertexArray _vao;
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

            _vbo = new VertexBuffer(_vertices);
            _vao = new VertexArray();
            _shader = new Shader("shader.vert", "shader.frag");
            
            _vao.SetVertexAttribute(_vbo, _shader.GetAttributeLocation("vPosition"), 3, 0);
            
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
            _vao.Bind();
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

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
            
            _vbo.Dispose();
            _shader.Dispose();
        }
    }
}