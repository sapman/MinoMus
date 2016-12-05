using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MinoMus.TwoDimensional
{
    public class Player
    {
        Texture2D texture;

        Vector2 Position;
        int TempRow;
        const float speed = 100;
        float movingSpeed;
        Animation Anime;

        KeyboardState keyboard;

        public Player(Texture2D Texture, Vector2 startingPosition, Vector2 AmountofFrames)
        {
            this.texture = Texture;
            this.Position = startingPosition;
            TempRow = 0;
            Anime = new Animation(texture, AmountofFrames);
        }

        public void Update(GameTime gameTime)
        {
            Anime.Update(gameTime);
            keyboard = Keyboard.GetState();

            movingSpeed = speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Anime.Active = true;
            if (keyboard.IsKeyDown(Keys.Down))
            {
                TempRow = 0;
                Position.Y += movingSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                TempRow = 1;
                Position.X -= movingSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                TempRow = 2;
                Position.X += movingSpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Up))
            {
                TempRow = 3;
                Position.Y -= movingSpeed;
            }
            else Anime.Active = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Anime.Draw(spriteBatch);
        }

    }
}
