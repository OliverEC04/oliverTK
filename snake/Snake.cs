using System.Collections.Generic;
using OpenTK.Mathematics;

namespace snake
{
    public class Snake
    {
        private List<Square> _body = new List<Square>();
        private Square test;
        
        public Snake(Vector2i boardSize)
        {
            Texture bodyTexture = new Texture("shit.jpg");
            
            // test = new Square(boardSize, new Vector2i(1, 1),
            //     bodyTexture);
            
            for (int i = 0; i < 5; i++)
            {
                _body.Add(new Square(boardSize, new Vector2i(boardSize.X / 2, boardSize.Y / 2 + i),
                    bodyTexture));
            }
        }
        
        public void Render()
        {
            for (int i = 0; i < _body.Count; i++)
            {
                _body[i].Render();
            }
        }
    }
}