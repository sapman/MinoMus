using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MinoMus.Menues
{
    public enum ColorChangeAxis { A, R, G, B };
    public enum ButtonType {Regular =1 , ColorChanging, BiggerSmaller, SmallerOnly, LeftRight};
    public class Button
    {
        public Color color = new Color(255,255,255);
        public Texture2D Texture;
        private Rectangle Rectangle;
        private bool Checker;
        private ButtonType type;
        private Rectangle Source;
        private SoundEffect Effect;
        private bool Play = false;
        public ColorChangeAxis Axis { get; set; }
        public Button(Texture2D Texture)
        {
            this.Texture = Texture;
            this.Rectangle = new Rectangle(0,0,Texture.Width,Texture.Height);
            type = ButtonType.Regular;
            Source = Rectangle;
            Axis = ColorChangeAxis.A;
        }
        public Button(Texture2D Texture, Rectangle Rectangle)
        {
            this.Texture = Texture;
            this.Rectangle = Rectangle;
            type = ButtonType.Regular;           
            Source = Rectangle;
            Axis = ColorChangeAxis.A;
        }

        public Button(Texture2D Texture, Vector2 Position)
        {
            this.Texture = Texture;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            type = ButtonType.Regular;
            Source = Rectangle;
            Axis = ColorChangeAxis.A;
        }
        public Button(Texture2D Texture, Rectangle Rectangle,SoundEffect Sound)
        {
            this.Texture = Texture;
            this.Rectangle = Rectangle;
            type = ButtonType.Regular;
            Source = Rectangle;
            Effect = Sound;
            Axis = ColorChangeAxis.A;
        }
        public Button(Texture2D Texture, Vector2 Position, SoundEffect Sound)
        {
            this.Texture = Texture;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            type = ButtonType.Regular;
            Source = Rectangle;
            Effect = Sound;
            Axis = ColorChangeAxis.A;
        }


        //Properties
        public ButtonType Type
        {
            get { return type; }
            set { type = value; }
        }
        public Vector2 Position
        {
            get { return new Vector2(Rectangle.X, Rectangle.Y); }
            set { this.Rectangle.X = (int)value.X;
                this.Rectangle.Y = (int)value.Y;
                Source = Rectangle; }
        }

        public void SetSize(Vector2 size)
        {
            this.Rectangle.Width = (int)size.X;
            this.Rectangle.Height = (int)size.Y;
            Source = Rectangle;
        }
        public bool Checked
        {
            get { return Checker; }
        }
        public Rectangle rectangele
        {
            get { return this.Rectangle; }
            set { Rectangle = value; Source = value; }
        }

        public Rectangle GetRectangle()
        {
            return this.Rectangle;
        }

        public void SetRectangle(Rectangle Rectangle)
        {
            this.Rectangle = Rectangle;
            Source = Rectangle;
        }

        public void SetPosition(Vector2 Position)
        {
            Rectangle.X = (int)Position.X;
            Rectangle.Y = (int)Position.Y;
            Source = Rectangle;
        }

        public bool Update()
        {

            MouseState mouse = Mouse.GetState();
            Rectangle MouseRectangle = new Rectangle(mouse.X, mouse.Y, 20, 20);
            if (Source.Intersects(MouseRectangle))
            {
                if (!Play && Effect != null )
                {
                    Play = true;
                    Effect.Play();
                }
                if (Type == ButtonType.LeftRight)
                    UpdateLeftRight();
                if(Type == ButtonType.BiggerSmaller)
                    UpdateBiggerSmaller();
                if(Type == ButtonType.ColorChanging)
                    UpdateColor();
                if (Type == ButtonType.SmallerOnly)
                    UpdateSmaller();
                if (mouse.LeftButton == ButtonState.Pressed)
                    Checker = true;
                else
                    if (Checker)
                    {
                        Checker = false;
                        Rectangle = Source;
                        return true;
                    }
            }
            else
            {
                Play = false;
                if(Type == ButtonType.BiggerSmaller || Type == ButtonType.SmallerOnly)
                    ResetBiggerSmaller();
                if(Type == ButtonType.LeftRight)
                    ResetLeftRight();
                if(Type == ButtonType.ColorChanging)
                    ResetColor();
                down = true;
                Checker = false;
            }
            return false;
        }


        public void UpdateEffects()
        {
            if (Type == ButtonType.LeftRight)
                UpdateLeftRight();
            if (Type == ButtonType.BiggerSmaller)
                UpdateBiggerSmaller();
            if (Type == ButtonType.ColorChanging)
                UpdateColor();
            if (Type == ButtonType.SmallerOnly)
                UpdateSmaller();
        }
        public void ReturnToBasicState()
        {
            if (Type == ButtonType.BiggerSmaller || Type == ButtonType.SmallerOnly)
                ResetBiggerSmaller();
            if (Type == ButtonType.LeftRight)
                ResetLeftRight();
            if (Type == ButtonType.ColorChanging)
                ResetColor();
        }

        public void Reset()
        {
            color = new Color(255, 255, 255);
            Rectangle = Source;
            Checker = false;
            down = false;
        }

        bool down = true;
        /// <summary>
        /// The value of the color in the selected movment axis
        /// </summary>
        public byte AxisValue
        {
            get
            {
                switch (Axis)
                {
                    case ColorChangeAxis.R:
                        return color.R;
                    case ColorChangeAxis.G:
                        return color.G;
                    case ColorChangeAxis.B:
                        return color.B;
                    default:
                        return color.A;
                }
            }
        }
        private void UpdateColor()
        {
            switch (Axis)
            {
                case ColorChangeAxis.R:
                    {
                        if (color.R >= 250) down = true;
                        if (color.R <= 0) down = false;
                        if (down) color.R -= 5; else color.R += 5;
                    } break;
                case ColorChangeAxis.G:
                    {
                        if (color.G >= 250) down = true;
                        if (color.G <= 0) down = false;
                        if (down) color.G -= 5; else color.G += 5;
                    }
                    break;
                case ColorChangeAxis.B:
                    {
                        if (color.B >= 250) down = true;
                        if (color.B <= 0) down = false;
                        if (down) color.B -= 5; else color.B += 5;
                    } break;
                default:
                    {
                        if (color.A >= 250) down = true;
                        if (color.A <= 0) down = false;
                        if (down) color.A -= 5; else color.A += 5;
                    }
                    break;
            }

        }
        private void ResetColor()
        {
            switch (Axis)
            {
                case ColorChangeAxis.R:
                    if (color.R <= 250)
                        color.R += 5;
                    break;
                case ColorChangeAxis.G:
                    if (color.G <= 250)
                        color.G += 5;
                    break;
                case ColorChangeAxis.B:
                    if (color.B <= 250)
                        color.B += 5;
                    break;
                default:
                    if (color.A <= 250)
                        color.A += 5;
                    break;
            }

        }

        private void UpdateSmaller()
        {
            Rectangle.Width =Source.Width /  2;
            Rectangle.Height =Source.Height/ 2;
            Rectangle.X = Source.Center.X - Rectangle.Width / 2;
            Rectangle.Y = Source.Center.Y - Rectangle.Height / 2;
        }

        private void UpdateBiggerSmaller()
        {
            float Ratio = Source.Height / Source.Width;
            if (Rectangle.Width >= (int)(Source.Width * 1.5)) { down = true; }
            if (Rectangle.Width <= Source.Width /2 ) { down = false; }
            if (down) { Rectangle.Width -= 3; Rectangle.Height-=1;  }
            else { Rectangle.Width += 3; Rectangle.Height += 1; }
            Rectangle.X = Source.Center.X - Rectangle.Width / 2;
            Rectangle.Y = Source.Center.Y - Rectangle.Height / 2;
        }
        private void ResetBiggerSmaller()
        {
            if (Rectangle.Width > Source.Width ) { Rectangle.Width -= 3; Rectangle.Height -= 1; }
            if (Rectangle.Width < Source.Width ) { Rectangle.Width += 3; Rectangle.Height += 1; }
            Rectangle.X = Source.Center.X - Rectangle.Width / 2;
            Rectangle.Y = Source.Center.Y - Rectangle.Height / 2;
        }

        private void UpdateLeftRight()
        {
            if (Rectangle.X >= Source.X + 50) down = true;
            if (Rectangle.X <= Source.X - 50) down = false;
            if (down) Rectangle.X -= 2; else Rectangle.X += 2;
        }
        private void ResetLeftRight()
        {
            if (Rectangle.X > Source.X) Rectangle.X-=2;
            if (Rectangle.X < Source.X) Rectangle.X+=2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, color);
        }
    }

}
