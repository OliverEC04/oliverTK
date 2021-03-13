using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Mathematics;

namespace snake
{
    public class Snake
    {
        public Direction Direction = Direction.Up;
        private int _tick;
        private List<Square> _body = new List<Square>();
        private List<Vector2i> _gridPositions = new List<Vector2i>();
        
        public Snake(Vector2 boardSize, Vector2i boardGridSize)
        {
            Texture bodyTexture = new Texture("shit.jpg");

            for (int i = 0; i < 5; i++)
            {
                _gridPositions.Add(new Vector2i(boardGridSize.X / 2, boardGridSize.Y / 2 + i));
                _body.Add(new Square(boardSize, boardGridSize, _gridPositions[i], bodyTexture));
            }
        }
        
        public void Render()
        {
            if (_tick % 10 == 0)
            {
                switch (Direction)
                {
                    case Direction.Right:
                        _gridPositions.Insert(0, new Vector2i(_gridPositions[0].X + 1, _gridPositions[0].Y));
                        break;
                    
                    case Direction.Left:
                        _gridPositions.Insert(0, new Vector2i(_gridPositions[0].X - 1, _gridPositions[0].Y));
                        break;
                
                    case Direction.Down:
                        _gridPositions.Insert(0, new Vector2i(_gridPositions[0].X, _gridPositions[0].Y + 1));
                        break;
                
                    case Direction.Up:
                        _gridPositions.Insert(0, new Vector2i(_gridPositions[0].X, _gridPositions[0].Y - 1));
                        break;
                }

                for (int i = 0; i < _body.Count; i++)
                {
                    _body[i].GridPosition = _gridPositions[i];
                }
            }

            for (int i = 0; i < _body.Count; i++)
            {
                _body[i].Render();
            }

            _tick++;
        }

        public void OnResize(Vector2 boardSize)
        {
            for (int i = 0; i < _body.Count; i++)
            {
                _body[i].OnResize(boardSize);
            }
        }
    }
}