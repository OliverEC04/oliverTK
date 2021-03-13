using OpenTK.Mathematics;

namespace snake
{
    public class Endscreen
    {
        private Rect _rect;
        
        public Endscreen()
        {
            _rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(1, 1), new Texture("shutdown.jpg"));
        }

        public void Render()
        {
            _rect.Render();
        }

        public void OnResize()
        {
            
        }
    }
}