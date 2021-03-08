using System.Numerics;
using OpenTK.Mathematics;
using Vector2 = OpenTK.Mathematics.Vector2;

namespace snake
{
    public class Fruit
    {
        private Rect _rect;
        public Fruit(Vector2 position, Vector2 size, Texture texture)
        {
            _rect = new Rect(position, size, texture);
        }

        public void Render()
        {
            _rect.Render();
        }
    }
}