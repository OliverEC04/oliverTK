using System;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace snake
{
    public class Rect
    {
        public Vector2 Position;
        public Vector2 Size;
        private static readonly float[] Vertices = new []
        {
            -1.0f, -1.0f,
            -1.0f,  1.0f,
             1.0f,  1.0f,
             1.0f, -1.0f,
        };
        private static readonly uint[] Indices = new uint[]
        {
            0, 1, 2,
            0, 2, 3,
        };
        private static readonly VertexBuffer Vbo = new VertexBuffer(Vertices);
        private static readonly ElementBuffer Ebo = new ElementBuffer(Indices);
        private static readonly VertexArray Vao = new VertexArray();
        private static readonly Shader Shader = new Shader("texShader.vert", "texShader.frag");
        private Vector2i _boardSize;
        private Texture _texture;
        private Vector2 _texRepeat;

        public Rect(Vector2 position, Vector2 size, Texture texture)
        {
            Position = position;
            Size = size;
            _texture = texture;
            _texRepeat = new Vector2(1.0f, 1.0f);
            
            Vao.SetVertexAttribute(Vbo, (uint) Shader.GetAttributeLocation("vPosition"), 2, 2,
                0);
            Vao.SetEbo(Ebo);
        }
        
        public Rect(Vector2 position, Vector2 size, Texture texture, Vector2 texRepeat)
        {
            Position = position;
            Size = size;
            _texture = texture;
            _texRepeat = texRepeat;
            
            Vao.SetVertexAttribute(Vbo, (uint) Shader.GetAttributeLocation("vPosition"), 2, 2,
                0);
            Vao.SetEbo(Ebo);
        }

        public unsafe void Render()
        {
            Shader.Bind();
            Shader.SetUniform(Shader.GetUniformLocation("uPosition"), Position);
            Shader.SetUniform(Shader.GetUniformLocation("uSize"), Size);
            Shader.SetUniform(Shader.GetUniformLocation("uTexRepeat"), _texRepeat);
            Shader.SetUniform(Shader.GetUniformLocation("uTexture"), 0);
            _texture.Bind(TextureUnit.Texture0);
            Vao.Bind();
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length,
                DrawElementsType.UnsignedInt, (void*) 0);
        }
        
        public static void Dispose()
        {
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
        }
    }
}