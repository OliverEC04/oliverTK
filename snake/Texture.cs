using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace snake
{
    public class Texture : IDisposable
    {
        private uint _handle;
        
        public unsafe Texture(string texturePath)
        {
            _handle = GL.GenTexture();
            Bitmap bitmap = new Bitmap(texturePath);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Bind();

            GL.TexImage2D(TextureTarget.Texture2d, 0, (int) PixelFormat.Rgba, data.Width, data.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, (void*) data.Scan0);
            bitmap.UnlockBits(data);
        
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Repeat);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Repeat);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            GL.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);
        
            GL.GenerateMipmap(TextureTarget.Texture2d);
        }

        public void Bind(TextureUnit unit = TextureUnit.Texture0)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2d, _handle);
        }

        public void Dispose()
        {
            GL.DeleteTextures(1, _handle);
        }
    }
}