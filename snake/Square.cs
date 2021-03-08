using OpenTK.Mathematics;

namespace snake
{
    public class Square
    {
        private Rect _rect;
        
        public Square(Vector2i boardSize, Vector2i position, Texture texture)
        {
            _rect = new Rect(new Vector2(position.X / boardSize.X, position.Y / boardSize.Y), 
                new Vector2(1 / boardSize.X, 1 / boardSize.Y), texture);
        }

        public void Render()
        {
            _rect.Render();
        }
    }
}