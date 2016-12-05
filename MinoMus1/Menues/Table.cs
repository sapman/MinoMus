using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.Menues
{
    public class TextTable
    {
        string[,] data;
        
        public bool DrawLines { get ; set; }

        Texture2D LineTexture;
        public Color LineColor { get; set; }
        public int LineThickness { get; set; }

        public Color TextColor { get; set; }
        public Color TitleColor { get; set; }

        Vector2 boxSize;
        Vector2 startingPosition;

        public SpriteFont Font { get; set; }
        public SpriteFont TitlesFont { get; set; }

        public TextTable(string[,] Data, Vector2 BoxSize, Vector2 StartingPosition , GraphicsDevice graphics, SpriteFont Font)
        {

            LineTexture = new Texture2D(graphics, 1, 1);
            LineTexture.SetData<Color>(new Color[] { Color.White });

            LineColor = Color.Black;
            TextColor = Color.Black;
            TitleColor = Color.Black;

            this.data = Data;
            int columns = data.GetLength(0);
            int rows = data.GetLength(1);

            startingPosition = StartingPosition;
            boxSize = BoxSize;

            this.Font = Font;
            this.TitlesFont = Font;

            LineThickness = 1;

        }

        public void AutoSetBoxSize()
        {
            float x = 0;
            float y = 0;

            foreach (var item in data)
            {
                x = Math.Max(x, TitlesFont.MeasureString(item).X);
                y = Math.Max(y, TitlesFont.MeasureString(item).Y);
            }

            this.boxSize = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            for (int c = 0; c < rows; c++)
            {
                for (int r = 0; r < columns; r++)
                {
                    spriteBatch.DrawString(c==0? TitlesFont : Font, data[c, r],
                        new Vector2(startingPosition.X + r  * boxSize.X, startingPosition.Y + c * boxSize.Y), c==0? TitleColor :TextColor);   
                }
            }

            if (DrawLines)
            {
                int width = columns * (int)boxSize.X;
                int height = rows * (int)boxSize.Y;

                for (int i = 0; i <= rows; i++)
                {
                    spriteBatch.Draw(LineTexture, new Rectangle((int)startingPosition.X - LineThickness, (int)startingPosition.Y + (i * (int)boxSize.Y) - LineThickness, width + LineThickness, LineThickness), LineColor);
                }

                for (int i = 0; i <= columns; i++)
                {
                    spriteBatch.Draw(LineTexture, new Rectangle((int)startingPosition.X + (i * (int)boxSize.X) - LineThickness, (int)startingPosition.Y, LineThickness, height), LineColor);
               
                }

            }
        }
    }
}
