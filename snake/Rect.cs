using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace snake
{
    public class Rect
    {
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
        
        private Texture _texture;
        private Vector2 _position;
        private Vector2 _size;

        public Rect(Vector2 position, Vector2 size, Texture texture)
        {
            _position = position;
            _size = size;
            _texture = texture;
            
            Vao.SetVertexAttribute(Vbo, Shader.GetAttributeLocation("vPosition"), 2, 2, 0);
            Vao.SetEbo(Ebo);
        }

        public void Render()
        {
            Shader.Bind();
            Shader.SetUniform(Shader.GetUniformLocation("uPosition"), _position);
            Shader.SetUniform(Shader.GetUniformLocation("uSize"), _size);
            Shader.SetUniform(Shader.GetUniformLocation("uTexture"), 0);
            _texture.Bind(TextureUnit.Texture0);
            Vao.Bind();
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length,
                DrawElementsType.UnsignedInt, 0);
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