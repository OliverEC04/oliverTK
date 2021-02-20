using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace oliverTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (GayMe spil = new GayMe(800, 400, "My GayMe"))
            {
                spil.Run();
            }
        }
    }
}
