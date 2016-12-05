using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinoMus.Menues
{
    public class NumPad
    {
        TextBox txt_Num;

        List<Button> btns;

        private Vector2 StartingPoint;

        private Rectangle Rectangle;

        public NumPad(List<Button> lst, SpriteFont Font, Rectangle rectangle, Texture2D Texture)
        {
            Rectangle = rectangle;
            StartingPoint = new Vector2(rectangle.X, rectangle.Y);
            btns = lst;
            Organize(rectangle, Font);
            txt_Num = new TextBox(Font, Texture, new Vector2(rectangle.X, rectangle.Y), rectangle.Width, (int)Font.MeasureString("G").Y);
        }

        private void Organize(Rectangle rect, SpriteFont Font)
        {
            int Y0 = rect.Y + (int)Font.MeasureString("8").Y + 10;
            int RowH = rect.Height / 5;
            int ColumnW = rect.Width / 3;

            foreach (var btn in btns)
            {
                btn.SetSize(new Vector2(7 * (RowH) / 10, 7 * (ColumnW) / 10));
            }

            int row = 0;
            int column = 0;
            for (int i = 1; i < 10; i++)
            {
                if (column > 2)
                {
                    row++;
                    column = 0;
                }
                btns[i].SetPosition(new Vector2(rect.X + (column * ColumnW), Y0 + row * RowH));
                column++;
            }
            btns[0].SetPosition(new Vector2(rect.X + ColumnW, Y0 + 3 * RowH));
            btns[10].SetPosition(new Vector2(rect.X, Y0 + 3 * RowH));
            btns[11].SetPosition(new Vector2(rect.X + 2 * ColumnW, Y0 + 3 * RowH));
        }

        public void SetPosition(Vector2 pos)
        {
            Rectangle.X = (int)pos.X;
            Rectangle.Y = (int)pos.Y;

            txt_Num.SetPosition(pos);
            for (int i = 0; i < 12; i++)
            {
                btns[i].SetPosition(btns[i].Position - StartingPoint);
                btns[i].SetPosition(btns[i].Position + pos);
            }

            StartingPoint = pos;
        }

        public void Update()
        {
            for (int i = 0; i < btns.Count; i++)
            {
                if (btns[i].Update())
                {
                    if (txt_Num.handler.Font.MeasureString(txt_Num.Text).X < txt_Num.Rectangle.Width - txt_Num.LetterWidth * 2)
                        if (i <= 9)
                        {
                            txt_Num.Text += i.ToString();
                        }
                    if (i == 10)
                    {
                        txt_Num.Text = "";
                    }
                    if (i == 11)
                    {
                        if (txt_Num.Text.Length > 0)
                            txt_Num.Text = txt_Num.Text.Remove(txt_Num.Text.Length - 1);
                    }
                    foreach (var btn in btns)
                    {
                        btn.Reset();
                    }
                    break;
                }
            }
        }

        public float Value { get {  return (txt_Num.Text != "")? float.Parse(txt_Num.Text) : 0 ; } set { txt_Num.Text = value.ToString(); } }

        public int Reset()
        {
            int num;
            if(this.txt_Num.Text != "")
                num = int.Parse(txt_Num.Text);
            else 
                num = 0;
            txt_Num.Text = "";
            return num;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            txt_Num.Draw(spriteBatch);
            foreach (var btn in btns)
            {
                btn.Draw(spriteBatch);
            }
        }
    }
}
