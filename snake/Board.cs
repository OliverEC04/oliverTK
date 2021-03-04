using OpenTK.Mathematics;

namespace snake
{
    public class Board : Rect
    {
        public Board(Vector2i position, Vector2i size, Texture texture) : base(position, size, texture)
        {
            
        }

        public void Render()
        {
            base.Render();
        }
    }
}