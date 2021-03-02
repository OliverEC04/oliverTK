using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace snake
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 400, "Snake"))
            {
                game.Run();
            }
        }
    }
}