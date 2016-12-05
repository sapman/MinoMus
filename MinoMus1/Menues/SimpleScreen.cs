using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MinoMus.Other;

namespace MinoMus.Menues
{
    public class SimpleScreen : Screen
    {

        public SimpleScreen(Texture2D BackgrGround, Button Back, Viewport Viewport)
        {
            Background = new Background(BackgrGround, Viewport);
            this.Back = Back;

            Back.SetRectangle(MinoMusSystem.newRectangle(new Vector2(Viewport.Width - Back.rectangele.Width - 100, Viewport.Height - Back.rectangele.Height - 50),
                Back.rectangele.Width, Back.rectangele.Height));
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);
            Back.Draw(spriteBatch);
        }
    }
}
