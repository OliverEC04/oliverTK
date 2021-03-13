using System;
using System.Numerics;
using OpenTK.Mathematics;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace snake
{
    public class Fruit
    {
        private Square _square;
        
        public Fruit(Vector2 boardSize, Vector2i boardGridSize, Vector2i gridPosition)
        {
            Random random = new Random();
            int texNum = random.Next(0, 1);
            Texture texture = new Texture("assets/4tile.png");
            
            switch (texNum)
            {
                case 0:
                    break;
                
                case 1:
                    texture = new Texture("assets/shit.jpg");
                    break;
            }
            
            _square = new Square(boardSize, boardGridSize, gridPosition, texture);
        }

        public void Render()
        {
            _square.Render();
        }

        public void OnResize(Vector2 boardSize)
        {
            _square.OnResize(boardSize);
        }
    }
}
