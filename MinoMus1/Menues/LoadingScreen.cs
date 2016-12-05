using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MinoMus.Other;

namespace MinoMus.Menues
{
    public class LoadingScreen
    {
        Texture2D Bar;
        Background BackGround;

        Rectangle Rectangle;
        Vector2 Position;

        double Timer;
        const double interval = 0.5;
        public LoadingScreen( Texture2D Bar, Texture2D BackGround, Viewport ViewPort)
        {
            this.Bar = Bar;
            this.BackGround = new Background(BackGround, ViewPort);
            Position = MinoMusSystem.GetCenter(ViewPort)- new Vector2(Bar.Width/2,0);
            Rectangle = MinoMusSystem.newRectangle(Position, 0, Bar.Height); 
        }

        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Timer >= interval)
            {
                Timer = 0;
                Rectangle.Width += 50;
            }
            if (Rectangle.Width >= Bar.Width)
                MinoMusSystem.GameState = GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BackGround.Draw(spriteBatch);
            spriteBatch.Draw(Bar, Position, Rectangle, Color.White);
        }
    }
}
