using System;
using OpenTK.Mathematics;

namespace snake
{
    public class Board
    {
        public Vector2i Size;
        private Rect _rect;
        
        public Board(Vector2i screenSize, Vector2i size)
        {
            Size = size;
            _rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(1.0f, 1.0f),
                new Texture("4tile.png"), (Vector2)size / 2);
            
            OnResize(screenSize);
        }

        public void Render()
        {
            _rect.Render();
        }

        public void OnResize(Vector2i screenSize)
        {
            Vector2 aspectRatio = new Vector2((float) screenSize.Y / screenSize.X + 1.0f,
                (float) screenSize.X / screenSize.Y + 1.0f);
            float divVal = aspectRatio.X > aspectRatio.Y ? aspectRatio.X : aspectRatio.Y;
            _rect.Size = new Vector2(aspectRatio.X / divVal, aspectRatio.Y / divVal);
        }
    }
}