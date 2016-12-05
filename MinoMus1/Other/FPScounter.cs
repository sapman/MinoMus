using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Other
{
    public class FPScounter
    {
        private SpriteFont spriteFont;
        private float FPS = 0f;
        private float totalTime;
        private float displayFPS;

        public Color Color = Color.Black;
        public Vector2 Position;

        public FPScounter(SpriteFont Font)
        {
            this.totalTime = 0f;
            this.displayFPS = 0f;
            this.spriteFont = Font;
            Position = new Vector2();
        }

        public FPScounter(SpriteFont Font, Vector2 Position) 
        {
            this.totalTime = 0f;
            this.displayFPS = 0f;
            this.spriteFont = Font;
            this.Position = Position;
        }

        public void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            totalTime += elapsed;

            if (totalTime >= 1000)
            {
                displayFPS = FPS;
                FPS = 0;
                totalTime = 0;
            }
            FPS++;
        }

        public void Draw( SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.spriteFont, this.displayFPS.ToString() + " FPS",Position, Color);
        }
    }
}
