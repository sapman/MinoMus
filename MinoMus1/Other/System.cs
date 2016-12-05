using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MinoMus.Other
{
    public enum GameState:int {Quiting,Loading,MainMenu,Options,Credits,Playing};
    public static class MinoMusSystem
    {
        //GameState + Property
        private static GameState gameState = GameState.Loading;
        public static GameState GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        //Returns The Vector Of The Center Of The Screen
        public static Vector2 GetCenter(Viewport ViewPort)
        {
            return new Vector2(ViewPort.Width / 2, ViewPort.Height / 2);
        }
        public static Vector2 GetCenter(GraphicsDevice graphics)
        {
            return new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2);
        }
        public static Vector2 GetCenter(Viewport Viewport, Texture2D Texture)
        {
            return new Vector2((Viewport.Width / 2), (Viewport.Height / 2));
        }
        public static Vector2 GetOrigin(Texture2D Texture)
        {
            return new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        //Retruns The Vector Of the Center Of A Rectangle
        public static Vector2 GetCenter(Rectangle Rectangle)
        {
            return new Vector2(Rectangle.Center.X, Rectangle.Center.Y);
        }

        //Gets Vector ,Width and height and returns A Rectangle
        public static Rectangle newRectangle(Vector2 Vector, int Width, int Height)
        {
            return new Rectangle((int)Vector.X, (int)Vector.Y, Width, Height);
        }

    }
}
