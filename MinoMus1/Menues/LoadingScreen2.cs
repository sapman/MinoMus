using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MinoMus.Other;

namespace MinoMus.Menues
{
    /// <summary>
    /// this Loading Screen Will Create A Rotating Object
    /// </summary>
    class LoadingScreen2 
    {
        Texture2D Obj;
        Background Background;

        Rectangle Rectangle;
        Vector2 Position;

        float rotation;

        double Timer;
        const double interval = 0.5;
        public LoadingScreen2( Texture2D Texture, Texture2D BackGround, Viewport ViewPort)
        {
            this.Obj = Texture;
            this.Background = new Background(BackGround, ViewPort);
            Position = MinoMusSystem.GetCenter(ViewPort, Texture);
            Rectangle = MinoMusSystem.newRectangle(Position, 0, Texture.Height); 
        }

        public void Update(GameTime gameTime)
        {
            Timer += gameTime.ElapsedGameTime.TotalSeconds;
            rotation += 0.5f;
            if (Timer >= 3)
                MinoMusSystem.GameState = GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Background.Draw(spriteBatch);

            spriteBatch.Draw(Obj, Position, null, Color.Red, rotation, MinoMusSystem.GetOrigin(Obj), 1, SpriteEffects.None, 1);
        }
    }
}
