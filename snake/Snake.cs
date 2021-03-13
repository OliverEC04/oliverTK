using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Mathematics;

namespace snake
{
    public class Snake
    {
        public Direction Direction = Direction.Up;
        public bool IsAlive = true;
        private int _tick;
        private Vector2i _boardGridSize;
        private List<Square> _body = new List<Square>();
        private List<Vector2i> _gridPositions = new List<Vector2i>();
        
        public Snake(Vector2 boardSize, Vector2i boardGridSize)
        {
            _boardGridSize = boardGridSize;
            Texture bodyTexture = new Texture("restart.png");
            Texture headTexture = new Texture("turnOff.png");

            for (int i = 0; i < 5; i++)
            {
                _gridPositions.Add(new Vector2i(boardGridSize.X / 2, boardGridSize.Y / 2 + i));

                if (i == 0)
                {
                    _body.Add(new Square(boardSize, boardGridSize, _gridPositions[i], headTexture));
                }
                else
                {
                    _body.Add(new Square(boardSize, boardGridSize, _gridPositions[i], bodyTexture));
                }
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

                if (0 > _body[0].GridPosition.X || _body[0].GridPosition.X >= _boardGridSize.X ||
                    0 > _body[0].GridPosition.Y || _body[0].GridPosition.Y >= _boardGridSize.Y)
                {
                    IsAlive = false;
                }
                
                for (int i = 1; i < _body.Count; i++)
                {
                    if (_body[0].GridPosition == _body[i].GridPosition)
                    {
                        IsAlive = false;
                    }
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