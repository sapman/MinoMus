using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MinoMus.TwoDimensional
{
    public class MovingObject : TextureInstance
    {
        public Keys Forwards { get; set; }
        public Keys Backwards { get; set; }
        public Keys RotateClockwise { get; set; }
        public Keys RotateUnClockwise { get; set; }

        public float Speed { get; set; }

        public MovingObject(Texture2D Texture2D ,Vector2 startingPosition, float startingRotation , float speed , Vector2 size)
        {
            texture = Texture2D;
            position = startingPosition;
            Rotation = startingRotation;
            Speed = speed;
            Size = size;

            BuildRectangle();
            SetOrigin();
        }

        public MovingObject(Texture2D Texture2D, Vector2 startingPosition, float startingRotation, float speed)
        {
            texture = Texture2D;
            position = startingPosition;
            Rotation = startingRotation;
            Speed = speed;
            Size = new Vector2(Texture2D.Width, Texture2D.Height);

            BuildRectangle();
            SetOrigin();
        }

        public void SetKeys(Keys forwards, Keys backwards, Keys Rotateclock, Keys RotateUnclock)
        {
            Forwards = forwards;
            Backwards = backwards;
            RotateClockwise = Rotateclock;
            RotateUnClockwise = RotateUnclock;
        }

        /// <summary>
        /// Sets the keys to arrow keys
        /// </summary>
        public void SetArrowKeys()
        {
            Forwards = Keys.Up;
            Backwards = Keys.Down;
            RotateClockwise = Keys.Right;
            RotateUnClockwise = Keys.Left;
        }

        /// <summary>
        /// Sets the keys to WASD
        /// </summary>
        public void SetWASD()
        {
            Forwards = Keys.W;
            Backwards = Keys.S;
            RotateClockwise = Keys.D;
            RotateUnClockwise = Keys.A;
        }

        public override void Update()
        {
            KeyboardState KeyboardState = Keyboard.GetState();

            if (KeyboardState.IsKeyDown(RotateClockwise)) Rotation += 0.01f * Speed;
            if (KeyboardState.IsKeyDown(RotateUnClockwise)) Rotation -= 0.01f * Speed;
            if (KeyboardState.IsKeyDown(Forwards)) this.position += new Vector2(Speed * (float)Math.Cos(Rotation), Speed * (float)Math.Sin(Rotation));
            if (KeyboardState.IsKeyDown(Backwards)) this.position -= new Vector2(Speed * (float)Math.Cos(Rotation), Speed * (float)Math.Sin(Rotation));

            BuildRectangle();
        }
    }
}
