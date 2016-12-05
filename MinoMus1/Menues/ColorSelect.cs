using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public enum ColorSelcetType
    {
        Openable, 
        Static
    }

    public class ColorSelect
    {
        List<Color> Colors;
        List<Rectangle> Rectangles;

        public Vector2 Position { get; set; }

        public bool ShowNames { get; set; }
        private int ReapetColumns;

        public int SelectedIndex { get; private set; }
        public Color SelectedColor { get { return Colors[SelectedIndex]; } }

        public Vector2 BoxSize { get; set; }
        public Vector2 Margins { get; set; }

        public bool IsOpen { get; private set; }

        public ColorSelcetType Type { get; private set; }

        private Texture2D Texture;

        /// <summary>
        /// Creates a select of colors
        /// </summary>
        /// <param name="colors">The Colors to be selectable</param>
        /// <param name="reapet">How many boxes to show in a row</param>
        public ColorSelect(List<Color> colors,Vector2 position, int reapet, Vector2 boxSize, Vector2 margin, ColorSelcetType type)
        {
            this.Position = position;
            this.Colors = colors;
            ShowNames = false;
            this.ReapetColumns = reapet;
            this.BoxSize = boxSize;
            this.Margins = margin;
            this.Type = type;
            this.SelectedIndex = -1;
            IsOpen = false;
            if (Type == ColorSelcetType.Openable)
                SelectedIndex = 0;

            Arrange();
        }
        /// <summary>
        /// Creates a defualt Color Select
        /// </summary>
        public ColorSelect()
        {
            this.Colors = GetDefault();
            ReapetColumns = Colors.Count;
            BoxSize = new Vector2(50, 50);
            Margins = new Vector2(10, 10);
            Type = ColorSelcetType.Static;
            this.SelectedIndex = -1;
            Arrange();
        }

        public void SetRepeats(int repeat)
        {
            this.ReapetColumns = repeat;
            Arrange();
        }

        public void SetToDefualtColors()
        {
            this.Colors = GetDefault();
            Arrange();
        }

        public void Update(Cursor gameCursor)
        {
            if ((Type == ColorSelcetType.Openable && IsOpen) || Type == ColorSelcetType.Static)
            {
                for (int i = 0; i < Rectangles.Count; i++)
                {
                    if (gameCursor.GetRectangle().Intersects(Rectangles[i]) && gameCursor.LeftButton)
                    {
                        SelectedIndex = i;
                        IsOpen = false;
                    }
                }
            }
            if(Type == ColorSelcetType.Openable)
            {
                Rectangle baseRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)BoxSize.X, (int)BoxSize.Y);
                if (gameCursor.Intersects(baseRectangle) && gameCursor.LeftButton)
                    IsOpen = !IsOpen;
            }
        }

        private void Arrange()
        {
            Rectangles = new List<Rectangle>();
            int j = 0;
            int i = 0;

            Vector2 startingPos = new Vector2(Position.X + BoxSize.X + Margins.X, Position.Y);
            foreach (var color in Colors)
            {
                Rectangles.Add(new Rectangle((int)startingPos.X + j * (int)(BoxSize.X + Margins.X),
                    (int)startingPos.Y + i * (int)(BoxSize.Y + Margins.Y), 
                    (int)BoxSize.X, (int)BoxSize.Y));
                
                j++;
                if (j == ReapetColumns)
                {
                    j = 0;
                    i++;
                }
            }
        }

        private static List<Color> GetDefault()
        {
            return new List<Color>() { Color.Red, Color.Blue, Color.Pink, Color.Violet, Color.Brown, Color.Orange, Color.Green, Color.Gray };
        }

        public Rectangle GetFullSizeRectangle()
        {
            int width = (int)((BoxSize.X +Margins.X ) * (ReapetColumns + 1));
            int height = (int)((BoxSize.Y + Margins.Y) * (Rectangles.Count / ReapetColumns));
            return new Rectangle((int)Position.X, (int)Position.Y, width, height);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            if (Texture == null)
                BuildTexture(graphics);
            if ((Type == ColorSelcetType.Openable && IsOpen) || Type == ColorSelcetType.Static)
            {
                for (int i = 0; i < Colors.Count; i++)
                {
                    spriteBatch.Draw(Texture, Rectangles[i], Colors[i]);
                }
            }
            if (SelectedIndex != -1)
                spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)BoxSize.X, (int)BoxSize.Y), Colors[SelectedIndex]);
        }

        private void BuildTexture(GraphicsDevice graphics)
        {
            Texture = new Texture2D(graphics, 50, 50);
            Color[] colors = new Color[2500];

            for (int i = 0; i < colors.Length; i++)
            {
                if (i < 50 || i > 2450 ||
                    i % 2500 < 100 || i % 2500 > 2400)
                    colors[i] = Color.Black;
                else
                    colors[i] = Color.White;
            }

            Texture.SetData<Color>(colors);
        }
    }
}
