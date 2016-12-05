using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinoMus.Menues
{
    public enum SizeType
    {
        Fixed, 
        AsText,
    }
    public class PopUp
    {
        private string Text;

        public bool IsOpen { get; private set; }

        private Texture2D BgTexture;

        private SpriteFont Font;

        private Rectangle Rectangle;

        public int Width { get { return Rectangle.Width; } }
        public int Height { get { return Rectangle.Height; } }

        public Color TextColour { get; set; }
        public Color BgColour { get; set; }
        public Color ButtonColour { get { return button.color; } set { button.color = value; } }

        private Button button;

        public PopUp(Texture2D Texture, SpriteFont font, Vector2 Position,Button btn, string Text, SizeType type)
        {
            TextColour = Color.White;
            button = btn;
            IsOpen = false;
            BgTexture = Texture;
            Font = font;
            this.Text = Text;
            if (type == SizeType.AsText)
                Rectangle = new Rectangle((int)Position.X, (int)Position.Y,
                    (int)font.MeasureString(Interpenter.GetLongestRow(Text)).X,
                    (int)font.MeasureString("S").Y * Interpenter.GetRowsCnt(Text) + 100);
            else
            {
                int x = (int)font.MeasureString(Interpenter.GetLongestRow(Text)).X;
                int y = (int)font.MeasureString("S").Y * Interpenter.GetRowsCnt(Text);

                int WH = Math.Max(x, y);

                if (WH == x)
                {
                    x = WH;
                    y = WH;// +100;
                }
                if (WH == y)
                {
                    x = WH;
                    y = WH;
                }
                Rectangle = new Rectangle((int)Position.X, (int)Position.Y, x, y);
            }
        }

        public void ToggleOpen()
        {
            IsOpen = !IsOpen;
        }

        public void Update()
        {
            button.SetRectangle(new Rectangle(Rectangle.Center.X - 50, Rectangle.Bottom - 100, 100, 100));
            if(IsOpen)
                if (button.Update())
                ToggleOpen();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsOpen)
            {
                spriteBatch.Draw(BgTexture, Rectangle, BgColour);
                spriteBatch.DrawString(Font, Text, new Vector2(Rectangle.X, Rectangle.Y), TextColour);
                button.Draw(spriteBatch);
            }
        }


    }
}
