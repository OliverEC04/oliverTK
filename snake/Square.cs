using System;
using OpenTK.Mathematics;

namespace snake
{
    public class Square
    {
        private Rect _rect;
        
        public Square(Vector2i boardSize, Vector2i position, Texture texture)
        {
            Vector2 aspectRatio = new Vector2((float) screenSize.Y / screenSize.X + 1.0f,
                (float) screenSize.X / screenSize.Y + 1.0f);
            float divVal = aspectRatio.X > aspectRatio.Y ? aspectRatio.X : aspectRatio.Y;
            _rect.Size = new Vector2(aspectRatio.X / divVal, aspectRatio.Y / divVal);
            
            
            _rect = new Rect(new Vector2((float) position.X / boardSize.X, (float) position.Y / boardSize.Y), 
                new Vector2(1.0f / boardSize.X * 0.9f, 1.0f / boardSize.Y * 0.9f), texture);
        }

        public void Render()
        {
            _rect.Render();
        }
    }
}