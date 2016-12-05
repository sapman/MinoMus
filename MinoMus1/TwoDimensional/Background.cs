using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinoMus.TwoDimensional;

namespace MinoMus
{
    public class Background
    {
        Texture2D texture;
        Rectangle Rectangle;

        public Background(Texture2D Texture, Camera cam)
        {
            this.texture = Texture;
            this.Rectangle = new Rectangle(0, 0, cam.view.Width, cam.view.Height); ;
        }
        public Background(Texture2D Texture, GraphicsDevice graphics)
        {
            this.texture = Texture;
            this.Rectangle = new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height);
        }
        public Background(Texture2D Texture, Viewport ViewPort)
        {
            this.texture = Texture;
            this.Rectangle = new Rectangle(0, 0, ViewPort.Width, ViewPort.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }
    }

    //public class MovingBackground
    //{
    //    private Texture2D bg;

    //    private Rectangle rect1;
    //    private Rectangle rect2;

    //    public MovingBackground(Texture2D texture, Viewport viewport)
    //    {
    //        bg = texture;
    //        rect1 = new Rectangle(viewport.Bounds.Left, 0,
    //            viewport.Width, viewport.Height);
    //        rect2 = new Rectangle(viewport.Bounds.Right + viewport.Width , 0,
    //            viewport.Width, viewport.Height);
    //    }

    //    public void Update(Camera cam)
    //    {
    //        if (rect1.X < cam.Center.X)
    //        {
    //            rect1.X = (int)cam.Center.X + cam.view.Width;
    //        }
    //        if (rect2.X < cam.Center.X)
    //        {
    //            rect2.X = (int)cam.Center.X + cam.view.Width;
    //        }
    //        if (rect1.X + cam.view.Width > cam.view.Bounds.Right)
    //        {
    //            rect1.X = cam.view.Bounds.Left - cam.view.Width;
    //        }
    //        if (rect2.X + cam.view.Width > cam.view.Bounds.Right)
    //        {
    //            rect2.X = cam.view.Bounds.Left - cam.view.Width;
    //        }
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        spriteBatch.Draw(bg, rect1, Color.White);
    //        spriteBatch.Draw(bg, rect2, Color.White);
    //    }
    //}

    public class MovingBackground
    {
        Rectangle Background1;
        Rectangle Background2;

        Texture2D BackgroundTexture;

        public MovingBackground(Texture2D Text, GraphicsDevice Graphics)
        {
            Background1 = new Rectangle(0, 0, Graphics.Viewport.Width, 600);
            Background2 = new Rectangle(Background1.Width, 0, Graphics.Viewport.Width, 600); ;

            BackgroundTexture = Text;
        }

        public MovingBackground(Texture2D Text, Camera cam)
        {
            Background1 = new Rectangle(0, 0, cam.view.Width, 600);
            Background2 = new Rectangle(Background1.Width, 0, cam.view.Width, 600); ;

            BackgroundTexture = Text;
        }

        public MovingBackground(Texture2D Text, Viewport view)
        {
            Background1 = new Rectangle(0, 0,view.Width, 600);
            Background2 = new Rectangle(Background1.Width, 0, view.Width, 600); ;

            BackgroundTexture = Text;
        }

        public void Update(Camera camera, GraphicsDevice Graphics)
        {
            if (camera.Pos.X > Background1.X + Background1.Width)
            {
                Background1.X = Background2.Width + Background2.X;
            }
            if (camera.Pos.X > Background2.X + Background2.Width)
            {
                Background2.X = Background1.Width + Background1.X;
            }
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(BackgroundTexture, Background1, Color.White);
            Spritebatch.Draw(BackgroundTexture, Background2, Color.White);
        }
    }
}
