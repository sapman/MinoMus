using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.TwoDimensional
{
    public class TextureInstance
    {
        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; BuildRectangle(); }
        }

        protected Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        protected Texture2D texture;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value;}
        }

        protected Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; BuildRectangle(); }
        }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        protected Vector2 Size;

        protected TextureInstance()
        {
            position = new Vector2();
            rectangle = new Rectangle();
            Rotation = 0f;
            Scale = 1;
        }

        public TextureInstance(Texture2D Texture2D ,Vector2 startingPosition, float startingRotation ,Vector2 size)
        {
            texture = Texture2D;
            position = startingPosition;
            Rotation = startingRotation;
            Size = size;

            BuildRectangle(); 
            SetOrigin();
        }

        public TextureInstance(Texture2D Texture2D, Vector2 startingPosition, float startingRotation)
        {
            texture = Texture2D;
            position = startingPosition;
            Rotation = startingRotation;
            Size = new Vector2(Texture2D.Width,Texture2D.Height);

            BuildRectangle();
            SetOrigin();
        }

        protected void SetOrigin()
        {
            origin = new Vector2(texture.Width / 2, Texture.Height / 2);
        }

        public virtual void BuildRectangle()
        {
            rectangle = new Rectangle((int)position.X,
                (int)position.Y,
                (int)Size.X,
                (int)Size.Y);
        }

        public virtual void Update()
        {
            BuildRectangle();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, null, Color.White, Rotation, origin, SpriteEffects.None, 1f);
        }


    }
}
