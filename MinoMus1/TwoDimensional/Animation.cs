using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.TwoDimensional
{
    public class Animation
    {
        int frameTimer;
        int interval;

        bool active;

        Vector2 currentFrame , position, amountofFrames;

        Rectangle sourceRect;

        Texture2D Texture;

        // Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector2 CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public int FrameWidth
        {
            get { return Texture.Width / (int)amountofFrames.X; }
        }
        public int FrameHeight
        {
            get { return Texture.Height / (int)amountofFrames.Y; }
        }
        public int Row
        {
            get { return (int)currentFrame.Y; }
            set { currentFrame.Y = Row; }
        }

        //Methods
        public Animation(Texture2D Texture, Vector2 AmountofFrames)
        {
            this.Texture = Texture;
            this.amountofFrames = AmountofFrames;
        }

        public void Update(GameTime gameTime)
        {
            if (active)
                frameTimer += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            else
            {
                frameTimer = 0;
                currentFrame.X = 0;
            }
             if (frameTimer >= interval)
            {
                currentFrame.X += FrameWidth;
                if (currentFrame.X >= Texture.Width)
                    currentFrame.X = 0;
            }
            sourceRect = new Rectangle((int)currentFrame.X, (int)currentFrame.Y * FrameHeight,
                FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, sourceRect, Color.White);
        }
    }
}
