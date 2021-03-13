using System;
using OpenTK.Mathematics;

namespace snake
{
    public class Square
    {
        public Vector2i GridPosition;
        private Vector2 _boardSize;
        private Vector2i _boardGridSize;
        private Vector2 _squareSize;
        private Rect _rect;
        
        public Square(Vector2 boardSize, Vector2i boardGridSize, Vector2i gridPosition, Texture texture)
        {
            _boardSize = boardSize;
            _boardGridSize = boardGridSize;
            GridPosition = gridPosition;
            _rect = new Rect(new Vector2(0, 0), new Vector2(0, 0), texture);
            
            OnResize(boardSize);
        }

        public void Render()
        {
            _rect.Position = new Vector2((1 - _boardSize.X) / 2 + _squareSize.X / 2 + GridPosition.X * _squareSize.X,
                (1 - _boardSize.Y) / 2 + _squareSize.Y / 2 + GridPosition.Y * _squareSize.Y);

            _rect.Render();
        }

        public void OnResize(Vector2 boardSize)
        {
            _boardSize = boardSize;
            _squareSize = new Vector2(_boardSize.X / _boardGridSize.X, _boardSize.Y / _boardGridSize.Y);

            _rect.Size = new Vector2(_squareSize.X * 0.8f, _squareSize.Y * 0.8f);
        }
    }
}