using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private Endscreen _endscreen;
        private List<Keys> _keyLock = new List<Keys>();
        
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
            _snake = new Snake(_board.Rect.Size, _board.GridSize);
            // _fruit = new Fruit(new Vector2(0.5f, 0.5f), new Vector2(0.75f, 0.75f), new Texture("xp.png"));
            _endscreen = new Endscreen();
            
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

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (_snake.IsAlive)
            {
                if ((e.Key == Keys.Right || e.Key == Keys.D) &&!
                    _keyLock.Any(str => str == Keys.Right || str == Keys.D) && 
                    _snake.Direction != Direction.Left)
                {
                    _snake.Direction = Direction.Right;
                }

                if ((e.Key == Keys.Left || e.Key == Keys.A) &&!
                    _keyLock.Any(str => str == Keys.Left || str == Keys.A) && 
                    _snake.Direction != Direction.Right)
                {
                    _snake.Direction = Direction.Left;
                }
                
                if ((e.Key == Keys.Down || e.Key == Keys.S) &&!
                    _keyLock.Any(str => str == Keys.Down || str == Keys.S) && 
                    _snake.Direction != Direction.Up)
                {
                    _snake.Direction = Direction.Down;
                }
                
                if ((e.Key == Keys.Up || e.Key == Keys.W) &&!
                    _keyLock.Any(str => str == Keys.Up || str == Keys.W) && 
                    _snake.Direction != Direction.Down)
                {
                    _snake.Direction = Direction.Up;
                }

                if (!_keyLock.Contains(e.Key))
                {
                    _keyLock.Add(e.Key);
                }
            }
            else
            {
                if (e.Key == Keys.Enter)
                {
                    Reset();
                }
            }
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);
            
            _keyLock.Remove(e.Key);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            Render();
        }

        private void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (_snake.IsAlive)
            {
                _board.Render();
                _snake.Render();
                // _fruit.Render();
            }
            else
            {
                _endscreen.Render();
            }
            
            Context.SwapBuffers();
        }

        private void Reset()
        {
            _snake = new Snake(_board.Rect.Size, _board.GridSize);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, Size.X, Size.Y);
            Render();
            _board.OnResize(Size);
            _snake.OnResize(_board.Rect.Size);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            
            Rect.Dispose();
        }
    }
}