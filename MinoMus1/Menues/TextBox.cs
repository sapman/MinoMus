using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Text.RegularExpressions;

namespace MinoMus.Menues
{
  public  enum TextboxType
    {
        Static,
        SpacingChange,
        Sizeable
    }
  public class TextBox
  {
      public TextHandler handler;

      public TextboxType Type { get; set; }

      public string Text { get { return handler.Text; } set { handler.Text = value; } }
      public Rectangle Rectangle;
      private Texture2D BackgroundTexture;
      public int MaxLength { get; set; }
      public int MinWidth { get; private set; }
      public int MinHeight { get; private set; }
      private int InitSpacing;
      public int LetterWidth { get; private set; }


      public TextBox(SpriteFont Font, Texture2D Texture, Vector2 position, int Width, int Height)
      {
          LetterWidth = (int)Font.MeasureString("G").X;
          MinHeight = Height;
          if (MinHeight < (int)Font.MeasureString("G").Y)
              MinHeight = (int)Font.MeasureString("G").Y;
          MinWidth = Width;// +LetterWidth * 2;
          Rectangle = new Rectangle((int)position.X, (int)position.Y, MinWidth, MinHeight);
          handler = new TextHandler(Font, Color.White, new Vector2(position.X + LetterWidth, position.Y));

          BackgroundTexture = Texture;
          MaxLength = 0;
          Type = TextboxType.Static;
          InitSpacing = (int)Font.Spacing;
      }

      public TextBox(SpriteFont Font, Texture2D Texture, Vector2 position, int maxLength)
      {
          LetterWidth = (int)Font.MeasureString("G").X;
          MinHeight = (int)Font.MeasureString("G").Y;
          MaxLength = maxLength;
          MinWidth = MaxLength * LetterWidth;
          Rectangle = new Rectangle((int)position.X, (int)position.Y, MinWidth, MinHeight);
          handler = new TextHandler(Font, Color.White, new Vector2(position.X + LetterWidth, position.Y));

          BackgroundTexture = Texture;
          Type = TextboxType.Static;
          InitSpacing = (int)Font.Spacing;
      }

      public void SetPosition(Vector2 position)
      {
          Rectangle.X = (int)position.X;
          Rectangle.Y = (int)position.Y;
          handler.Position = new Vector2(Rectangle.X + LetterWidth, Rectangle.Y);
      }

      public void SetPosition(int X, int Y)
      {
          Rectangle.X = (int)X;
          Rectangle.Y = (int)Y;
          handler.Position = new Vector2(Rectangle.X + LetterWidth, Rectangle.Y);
      }

      public void CenterPosition(Vector2 screenCentre)
      {
          Rectangle.X = (int)screenCentre.X - (Rectangle.Width / 2);
          Rectangle.Y = (int)screenCentre.Y + (Rectangle.Height / 2);
          handler.Position = new Vector2(Rectangle.X + LetterWidth, Rectangle.Y);
      }

      public void Update()
      {
          if (Type == TextboxType.Static)
          {
              if (MaxLength != 0)
                  Rectangle.Width = (MaxLength + 2) * (int)handler.Font.MeasureString("G").X;

              handler.Update();

              while (handler.Text.Length > MaxLength)
              {
                  handler.Text = handler.Text.Remove(handler.Text.Length - 1);
              }
          }
          else if (Type == TextboxType.SpacingChange)
          {
              while ((int)handler.Font.MeasureString(handler.Text).X >= Rectangle.Width - ((int)handler.Font.MeasureString("G").X * 2))
              {
                  handler.Font.Spacing--;
              }
              while ((int)handler.Font.MeasureString(handler.Text).X < Rectangle.Width - ((int)handler.Font.MeasureString("G").X * 2) &&
                  handler.Font.Spacing < InitSpacing)
              {
                  handler.Font.Spacing++;
              }
              handler.Update();

              if (MaxLength != 0)
              {
                  while (handler.Text.Length > MaxLength)
                  {
                      handler.Text = handler.Text.Remove(handler.Text.Length - 1);
                  }
              }
          }
          else if (Type == TextboxType.Sizeable)
          {
              if ((int)handler.Font.MeasureString(handler.Text).X > MinWidth)
              {
                  Rectangle.Width = (int)handler.Font.MeasureString(handler.Text).X + 5;
              }
              else
              {
                  Rectangle.Width = MinWidth;
              }
              if ((int)handler.Font.MeasureString(handler.Text).Y > MinHeight)
              {
                  Rectangle.Height = (int)handler.Font.MeasureString(handler.Text).Y + 5;
              }
              else
              {
                  Rectangle.Height = MinHeight;
              }
              handler.Update();
          }

          //Rectangle.X = (int)handler.Position.X - (int)handler.Font.MeasureString("G").X;
          //Rectangle.Y = (int)handler.Position.Y;

      }

      public int GetMaxCapacity()
      {
          return (Rectangle.Width / (int)handler.Font.MeasureString("0").X) - 4;
      }

      public void Draw(SpriteBatch spriteBatch)
      {
          spriteBatch.Draw(BackgroundTexture, Rectangle, Color.White);
          handler.Draw(spriteBatch);
      }
  }

    public static class Interpenter
    {
        public static int GetRowsCnt(string Text)
        {
            return Regex.Split(Text, "\n").Length;
        }
        public static string[] GetRows(string Text)
        {
            return Regex.Split(Text, "\n");
        }
        public static string GetLongestRow(string Text)
        {
            string Longest = "";
            string[] collec = GetRows(Text);
            foreach (var item in collec)
            {
                if (Longest.Length < item.Length)
                    Longest = item;
            }
            return Longest;
        }
        public static int GetWordsCnt(string Text)
        {
            return Regex.Matches(Text, @"[a-zA-Z0-9]+").Count;
        }
        public static string[] GetWords(string Text)
        {
            return Regex.Matches(Text, @"[a-zA-Z0-9]+").OfType<Match>().Select(m => m.Groups[0].Value).ToArray();
        }
    }
}

