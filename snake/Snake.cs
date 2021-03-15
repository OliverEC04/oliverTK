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
        public List<Square> Body = new List<Square>();
        private int _tick;
        private Vector2 _boardSize;
        private Vector2i _boardGridSize;
        private Fruit _fruit;
        private Texture _bodyTexture;
        private List<Vector2i> _gridPositions = new List<Vector2i>();
        
        public Snake(Vector2 boardSize, Vector2i boardGridSize, Fruit fruit)
        {
            _boardSize = boardSize;
            _boardGridSize = boardGridSize;
            _fruit = fruit;
            _bodyTexture = new Texture("assets/restart.png");
            Texture headTexture = new Texture("assets/turnOff.png");

            for (int i = 0; i < 5; i++)
            {
                _gridPositions.Add(new Vector2i(boardGridSize.X / 2, boardGridSize.Y / 2 + i));

                if (i == 0)
                {
                    Body.Add(new Square(boardSize, boardGridSize, _gridPositions[i], headTexture));
                }
                else
                {
                    Body.Add(new Square(boardSize, boardGridSize, _gridPositions[i], _bodyTexture));
                }
            }
        }
        
        public void Render()
        {
            if (_tick % 10 == 0)
            {
                Move();
                CheckIfAlive();
                CheckFruitCollision();
            }

            for (int i = 0; i < Body.Count; i++)
            {
                Body[i].Render();
            }

            _tick++;
        }

        public void OnResize(Vector2 boardSize)
        {
            for (int i = 0; i < Body.Count; i++)
            {
                Body[i].OnResize(boardSize);
            }
        }

        private void CheckFruitCollision()
        {
            if (Body[0].GridPosition == _fruit.Square.GridPosition)
            {
                Body.Add(new Square(_boardSize, _boardGridSize, _gridPositions[Body.Count],
                    _bodyTexture));
                _fruit.Eat();
            }
        }

        private void Move()
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

            for (int i = 0; i < Body.Count; i++)
            {
                Body[i].GridPosition = _gridPositions[i];
            }
        }

        private void CheckIfAlive()
        {
            if (0 > Body[0].GridPosition.X || Body[0].GridPosition.X >= _boardGridSize.X ||
                0 > Body[0].GridPosition.Y || Body[0].GridPosition.Y >= _boardGridSize.Y)
            {
                IsAlive = false;
            }
                
            for (int i = 1; i < Body.Count; i++)
            {
                if (Body[0].GridPosition == Body[i].GridPosition)
                {
                    IsAlive = false;
                }
            }
        }
    }
}