using System;
using OpenTK.Mathematics;

namespace snake
{
    public class Board
    {
        public Vector2i GridSize;
        public Rect Rect;
        
        public Board(Vector2i screenSize, Vector2i gridSize)
        {
            GridSize = gridSize;
            Rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(1.0f, 1.0f),
                new Texture("4tile.png"), (Vector2)GridSize / 2);
            
            OnResize(screenSize);
        }

        public void Render()
        {
            Rect.Render();
        }

        public void OnResize(Vector2i screenSize)
        {
            Vector2 aspectRatio = new Vector2((float) screenSize.Y / screenSize.X + 1.0f,
                (float) screenSize.X / screenSize.Y + 1.0f);
            float divVal = aspectRatio.X > aspectRatio.Y ? aspectRatio.X : aspectRatio.Y;
            Rect.Size = new Vector2(aspectRatio.X / divVal, aspectRatio.Y / divVal);
        }

        // public void AddSquareObject(Fruit fruit, Vector2i position)
        // {
        //     
        // }
        //
        // public void AddSquareObject(Snake snake, Vector2i position)
        // {
        //     
        // }
    }
}