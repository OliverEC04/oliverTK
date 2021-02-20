using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace oliverTK
{
    public class GayMe : GameWindow 
    {
        public GayMe(int width, int height, string title)
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
             
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            KeyboardState key = KeyboardState;

            if (key.IsKeyDown(Keys.Escape))
            {
                Close();
            }

            base.OnUpdateFrame(args);
        }
    }
}