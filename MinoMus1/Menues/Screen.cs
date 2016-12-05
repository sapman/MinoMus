using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MinoMus.Other;

namespace MinoMus.Menues
{
    public class Screen
    {
        protected Background Background;
        protected Button Back;
        protected SpriteFont titleFont;
        SpriteFont textFont;
        string title;
        string text;
        Viewport viewPort;

        Vector2 titlePositon;
        Vector2 textPosition;
        Rectangle ButtonRectangle;

        Color color;
        protected Screen()
        { }
        public Screen(Texture2D Texture, Button btn_Back, SpriteFont titleFont,SpriteFont textFont, string Title, string Text , Viewport Viewport)
        {
            this.Background = new Background(Texture, viewPort) ;
            this.Back = btn_Back;
            this.titleFont = titleFont;
            this.textFont = textFont;
            this.title = Title;
            this.text = Text;
            this.viewPort = Viewport;
            Initialize();
        }

        private void Initialize()
        {
            color = Color.Black;
            titlePositon = new Vector2(viewPort.Width / 2 - title.Length * 12, 10);
            textPosition = new Vector2(100, 100);
            ButtonRectangle = new Rectangle(viewPort.Width - Back.GetRectangle().Width, viewPort.Height - Back.GetRectangle().Height, 150, 50);
            Back.rectangele = ButtonRectangle;
            Back.Type = ButtonType.Regular;
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public ButtonType ButtonType
        {
            set { Back.Type = value; }
        }

        public void Update()
        {
            if (Back.Update())
                MinoMusSystem.GameState = GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color c = Color.Gray;
            for (int i = 0; i < 10; i++)
			{
                spriteBatch.DrawString(textFont, title, new Vector2(titlePositon.X - i * 2, titlePositon.Y - i), c);
                c.R += 3;
                c.G += 2;
                c.B += 1;
            }
            spriteBatch.DrawString(textFont, title, new Vector2(titlePositon.X, titlePositon.Y), color);
            spriteBatch.DrawString(textFont, text, textPosition, color);
            Background.Draw(spriteBatch);
            Back.Draw(spriteBatch);
        }
    }
}
