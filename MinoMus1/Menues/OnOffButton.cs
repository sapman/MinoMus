using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MinoMus.Menues
{
    public class OnOffButton
    {
        Texture2D OnTexture;
        Texture2D OffTexture;

        public Rectangle Rectangle { get; set; }

        bool IsOn;

        private bool Checker;

        public OnOffButton(Texture2D on, Texture2D off, Rectangle rectanlge , bool StartingValue)
        {
            this.OffTexture = off;
            this.OnTexture = on;
            this.Rectangle = rectanlge;
            IsOn = StartingValue;
        }

        public bool Update()
        {
            MouseState mouse = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(mouse.X, mouse.Y, 20, 20);
            if (Rectangle.Intersects(MouseRectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                    Checker = true;
                else
                    if (Checker)
                    {
                        Checker = false;
                        IsOn = !IsOn;
                    }
            }
            else
                Checker = false;
            return IsOn;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(IsOn ? OnTexture : OffTexture, Rectangle, Color.White);
        }



    }
}
