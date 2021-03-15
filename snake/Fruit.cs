using System;
using System.Numerics;
using OpenTK.Mathematics;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace snake
{
    public class Fruit
    {
        public Square Square;
        private Vector2i _boardGridSize;
        private Random _random = new Random();
        
        public Fruit(Vector2 boardSize, Vector2i boardGridSize, Vector2i gridPosition)
        {
            _boardGridSize = boardGridSize;
            int texNum = _random.Next(0, 1);
            Texture texture = new Texture("assets/4tile.png");
            
            switch (texNum)
            {
                case 0:
                    break;
                
                case 1:
                    texture = new Texture("assets/shit.jpg");
                    break;
            }
            
            Square = new Square(boardSize, boardGridSize, gridPosition, texture);
        }

        public void Render()
        {
            Square.Render();
        }

        public void OnResize(Vector2 boardSize)
        {
            Square.OnResize(boardSize);
        }

        public void Eat()
        {
            Square.GridPosition = new Vector2i(_random.Next(0, _boardGridSize.X),
                _random.Next(0, _boardGridSize.Y));
        }
    }
}
