using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
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
             0.0f,  0.0f,  0.0f,   1.0f, 0.2f, 0.6f,
            -0.4f, -0.5f,  0.0f,   0.9f, 0.2f, 1.0f,
            -0.5f,  0.0f,  0.0f,   0.3f, 0.8f, 0.0f,
            -0.4f,  0.5f,  0.0f,   0.3f, 0.8f, 0.0f,
             0.4f,  0.5f,  0.0f,   0.3f, 0.1f, 0.6f,
             0.5f,  0.0f,  0.0f,   0.4f, 0.8f, 0.0f,
             0.4f, -0.5f,  0.0f,   0.3f, 0.2f, 0.2f,
        };

        private readonly uint[] _indices = new uint[]
        {
            0, 1, 2,
            0, 2, 3,
            0, 3, 4,
            0, 4, 5,
            0, 5, 6,
            0, 6, 1,
        };

        private VertexBuffer _vbo;
        private ElementBuffer _ebo;
        private VertexArray _vao;
        private Shader _shader;
        private readonly Stopwatch _stopwatch = new Stopwatch();

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
            _ebo = new ElementBuffer(_indices);
            _vao = new VertexArray();
            _shader = new Shader("shader.vert", "shader.frag");
            
            _vao.SetVertexAttribute(_vbo, _shader.GetAttributeLocation("vPosition"), 3, 6, 0);
            _vao.SetVertexAttribute(_vbo, _shader.GetAttributeLocation("vColor"), 3, 6, 3);
            _vao.SetEbo(_ebo);
            
            _stopwatch.Start();

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

            float red = (float) Math.Abs(Math.Cos(_stopwatch.ElapsedMilliseconds * .001));
            float green = (float) Math.Abs(Math.Sin(_stopwatch.ElapsedMilliseconds * .001));
            float blue = (float) Math.Abs(Math.Tan(_stopwatch.ElapsedMilliseconds * .001));
            _shader.SetUniform3("uColor", new Vector3(red, green, blue));

            _shader.SetUniform1("uScale",
                (float)Math.Abs(Math.Tan(_stopwatch.ElapsedMilliseconds * .0001)));
            
            _vao.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length,
                DrawElementsType.UnsignedInt, 0);
            
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
            _ebo.Dispose();
            _vao.Dispose();
            _shader.Dispose();
        }
    }
}