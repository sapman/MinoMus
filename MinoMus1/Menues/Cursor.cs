using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MinoMus.TwoDimensional;

namespace MinoMus.Menues
{
    public class Cursor
    {
        private MouseState LastMouseStat;
        private MouseState MouseS ;
        private Rectangle Rectangle;
        public Color Colour { get; set; }

        private float Ratio;


        private Texture2D Texture;

        public Cursor(Texture2D Texture)
        {
            MouseS = Mouse.GetState();
            Rectangle = new Rectangle(MouseS.X-20, MouseS.Y-20, 20, 20);
            this.Texture = Texture;
            Colour = Color.White;

        }

        public Cursor(Texture2D Texture ,int Width ,int Height)
        {
            MouseS = Mouse.GetState();
            Rectangle = new Rectangle(MouseS.X - Width / 2, MouseS.Y - Height /2, Width, Height);
            this.Texture = Texture;
            Colour = Color.White;
        }

        public Rectangle GetRectangle()
        {
            return this.Rectangle;
        }

        public MouseState GetState()
        {
            return MouseS;
        }

        public void SetSize(int Width, int Hegiht)
        {
            Rectangle.Width = Width;
            Rectangle.Height = Hegiht;
        }

        public void Init(Camera cam)
        {
            Ratio = 20 / (float)cam.view.Width;
        }


        // Buttons Checker
        #region Mouse Variables
        public bool LeftButtonClick { get { return MouseS.LeftButton == ButtonState.Pressed; } }
        public bool RightButtonClick { get { return MouseS.RightButton == ButtonState.Pressed; } }
        public bool LeftButton { get; private set; }
        public bool RightButton { get; private set; }
        public bool ScrollingUp { get; private set; }
        public bool ScrollingDown { get; private set; }
        private void UpdateScroll()
        {
            if (MouseS.ScrollWheelValue > LastMouseStat.ScrollWheelValue)
            {
                ScrollingUp = true;
                ScrollingDown = false;
            }
            else if (MouseS.ScrollWheelValue < LastMouseStat.ScrollWheelValue)
            {
                ScrollingDown = true;
                ScrollingUp = false;
            }
            else
            {
                ScrollingUp = false;
                ScrollingDown = false;
            }
        }
        #endregion

        public void Update(Camera cam)
        {
            MouseS = Mouse.GetState();

            Vector2 mouse = new Vector2(MouseS.X, MouseS.Y);
            mouse = Vector2.Transform(mouse, cam.InverseTransform);

            Rectangle.Width = (int)(cam.view.Width / cam.Zoom * Ratio);
            Rectangle.Height = (int)(cam.view.Width / cam.Zoom * Ratio);

            Rectangle.X = (int)mouse.X - (Rectangle.Width);
            Rectangle.Y = (int)mouse.Y - (Rectangle.Height);

            UpdateScroll();

            UpdateButtons();

            LastMouseStat = MouseS;
        }

        private void UpdateButtons()
        {
            RightButton = (MouseS.RightButton == ButtonState.Pressed) &&
                   (LastMouseStat.RightButton != ButtonState.Pressed);

            LeftButton = (MouseS.LeftButton == ButtonState.Pressed) &&
                  (LastMouseStat.LeftButton != ButtonState.Pressed);
        }

        public void Update()
        {
            MouseS = Mouse.GetState();
            Rectangle = new Rectangle((int)MouseS.X, (int)MouseS.Y, 20, 20);

            UpdateScroll();

            UpdateButtons();

            LastMouseStat = MouseS;
        }

        public bool Intersects(Rectangle Rectangle)
        {
            return (this.Rectangle.Intersects(Rectangle));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Colour);
        }
    }
}
