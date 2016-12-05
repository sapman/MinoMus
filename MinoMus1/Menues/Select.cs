using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public enum PositionType { SideAndDown, Down}
    public class Select
    {
        public string SelectedValue { get; private set; }

        List<string> Data;

        List<Rectangle> rects;

        public bool IsOpen { get; private set; }

        public bool IsFllowingX { get; set; }

        public bool IsFllowingY { get; set; }

        SpriteFont Font;

        private PositionType Type;

        public Color Colour { get; set; }

        public Select(List<string> lst, Vector2 startingPosition, SpriteFont font,PositionType Type)
        {
            Data = lst;
            SelectedValue = Data[0];
            rects = new List<Rectangle>();
            Font = font;
            this.Type = Type;
            Init(startingPosition);
            Colour = Color.White;
        }

        private void Init(Vector2 startingPosition)
        {
            int RectH = (int)Font.MeasureString("G").Y;

            rects.Add(new Rectangle((int)startingPosition.X, (int)startingPosition.Y,
                (int)Font.MeasureString(SelectedValue).X, RectH));
            if (Type == PositionType.Down)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    rects.Add(new Rectangle((int)startingPosition.X, (int)startingPosition.Y + (RectH * (i + 1)),
                        (int)Font.MeasureString(Data[i]).X, RectH));
                }
            }
            else if (Type == PositionType.SideAndDown)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    rects.Add(new Rectangle((int)startingPosition.X + rects[0].Width + 10, (int)startingPosition.Y + (RectH * (i)),
                        (int)Font.MeasureString(Data[i]).X, RectH));
                }
            }
        }

        private void SetPosition(Vector2 Position)
        {
            int RectH = (int)Font.MeasureString("G").Y;

            rects[0] = (new Rectangle((int)Position.X, (int)Position.Y,
                (int)Font.MeasureString(SelectedValue).X, RectH));
            if (Type == PositionType.Down)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    rects[i + 1] = (new Rectangle((int)Position.X, (int)Position.Y + (RectH * (i +1)),
                        (int)Font.MeasureString(Data[i]).X, RectH));
                }
            }
            else if (Type == PositionType.SideAndDown)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    rects[i + 1] = (new Rectangle((int)Position.X + rects[0].Width  + 10, (int)Position.Y + (RectH * (i)),
                        (int)Font.MeasureString(Data[i]).X, RectH));
                }
            }
        }

        public Vector2 StartingPoint { get { return new Vector2(rects[0].X, rects[0].Y); } set { SetPosition(value); } }

        public void Close(){ IsOpen = false; }

        public int GetMaxWidth()
        {
            int Max = 0;
            foreach (var rect in rects)
            {
                Max = Math.Max(rect.Width, Max);
            }
            return Max;
        }

        public void Reset()
        {
            SelectedValue = Data[0];
            IsOpen = false;
        }

        public int SelectedIndex { 
            get
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i] == SelectedValue)
                        return i;
                }
                return 0;
            }
        }

        public bool Update(Cursor c)
        {
            if (IsOpen)
            {
                if (rects[0].Intersects(c.GetRectangle()) && c.LeftButton)
                    IsOpen = false;
                for (int i = 0; i < Data.Count; i++)
                {
                    if (rects[i + 1].Intersects(c.GetRectangle()) && c.LeftButton)
                    {
                        SelectedValue = Data[i];
                        IsOpen = false;
                        break;
                    }
                }
            }
            else
            {
                if (rects[0].Intersects(c.GetRectangle()) && c.LeftButton)
                {
                    IsOpen = true;
                }
            }

            return IsOpen;
        }

        public void ToggleOpen(bool open)
        {
            IsOpen = open;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsOpen)
            {
                spriteBatch.DrawString(Font, SelectedValue, new Vector2(rects[0].X, rects[0].Y), Colour);
                for (int i = 0; i < Data.Count; i++)
                {
                    spriteBatch.DrawString(Font, Data[i], new Vector2(rects[i + 1].X, rects[i + 1].Y), Colour);
                }
            }
            else
            {
                spriteBatch.DrawString(Font, SelectedValue, new Vector2(rects[0].X, rects[0].Y), Colour);
            }
        }
    }
}
