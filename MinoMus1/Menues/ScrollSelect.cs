using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public class ScrollSelect
    {
        Texture2D Back;
        Texture2D Cursor;

        Rectangle BackRect;
        Rectangle CursorRect;

        public Color BarColor = Color.White; 
        public Color CursorColor = Color.White; 

        public float Value { get; private set; }

        public Rectangle Rectangle { get { return BackRect; } }

        public ScrollSelect(Texture2D back, Texture2D cursor, Rectangle rectangle, float StartingValue)
        {
            Back = back;
            Cursor = cursor;
            BackRect = rectangle;
            Value = StartingValue;

            CursorRect = new Rectangle(BackRect.X, BackRect.Y, BackRect.Width / 10, BackRect.Height);

            SetStartinPosition();
        }

        private void SetStartinPosition()
        {
            CursorRect.X = (int)(((float)BackRect.Width - (float)CursorRect.Width) * Value + BackRect.X);
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(BackRect.X, BackRect.Y);
            }
            set
            {
                CursorRect.X -= BackRect.X;
                BackRect.X = (int)value.X;
                BackRect.Y = (int)value.Y;
                CursorRect.Y = (int)value.Y;
                CursorRect.X += BackRect.X;
            }
        }

        public int Height { get { return BackRect.Height; } }
        public int Width { get { return BackRect.Width; } }

        public void Update(Cursor cursor)
        {
            Rectangle mouseRect = cursor.GetRectangle();

            if (CursorRect.Intersects(mouseRect) && cursor.LeftButtonClick)
            {
                CursorRect.X = mouseRect.X;
            }
            else if (BackRect.Intersects(mouseRect) && cursor.LeftButtonClick)
            {
                CursorRect.X = mouseRect.X;
            }

            if (CursorRect.X + CursorRect.Width > BackRect.X + BackRect.Width)
                CursorRect.X = BackRect.X + BackRect.Width - CursorRect.Width;
            if (CursorRect.X + CursorRect.Width < BackRect.X)
                CursorRect.X = BackRect.X - CursorRect.Width;


            // Value Calc
            // !BackRect.Width == 100%!
            Value = ((float)CursorRect.X - (float)BackRect.X) /
                    ((float)BackRect.Width - (float)CursorRect.Width);

            if (Value < 0)
            {
                Value = 0;
                SetStartinPosition();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Back, BackRect, BarColor);
            spriteBatch.Draw(Cursor, CursorRect, CursorColor);
        }


    }
}
