using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace snake
{
    public class Game : GameWindow
    {
        private Board _board;
        private Snake _snake;
        private Fruit _fruit;
        
        public Game(int width, int height, string title)
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(1.0f, 0.3f, 0.3f, 1.0f);

            _board = new Board(Size, new Vector2i(15, 15));
            _snake = new Snake(_board.Size);
            // _fruit = new Fruit(new Vector2(0.5f, 0.5f), new Vector2(0.75f, 0.75f), new Texture("xp.png"));
            
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

            _board.Render();
            _snake.Render();
            // _fruit.Render();
            
            Context.SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, Size.X, Size.Y);
            Render();
            _board.OnResize(Size);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            
            Rect.Dispose();
        }
    }
}