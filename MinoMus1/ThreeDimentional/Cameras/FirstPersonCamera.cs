using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional.Cameras
{
    public class FirstPersonCamera : Camera3D
    {
        public Keys Forward { get; set; }
        public Keys Backward { get; set; }
        public Keys Rigth { get; set; }
        public Keys Left { get; set; }

        public float Speed { get; set; }

        public float MouseSensitivity { get; set; }


        public FirstPersonCamera(Vector3 startingPosition, Vector3 startingRotation, GraphicsDevice graphics, float speed)
        {
            this.position = startingPosition;
            this.rotation = startingRotation;
            Speed = speed;
            Graphics = graphics;

            Vector2 Center = new Vector2(Graphics.Viewport.Width / 2, Graphics.Viewport.Height / 2);
            Mouse.SetPosition((int)Center.X, (int)Center.Y);

            MouseSensitivity = 5;

            CreateProjection();
            CreateView();
        }

        public void SetKeys(Keys forward, Keys backward, Keys rigth, Keys left)
        {
            Forward = forward;
            Backward = backward;
            Rigth = rigth;
            Left = left;
        }

        public void SetKeysWASD()
        {
            SetKeys(Keys.W, Keys.S, Keys.D, Keys.A);
        }
        public void SetKeysArrows()
        {
            SetKeys(Keys.Up, Keys.Down, Keys.Right, Keys.Left);
        }

        private Vector2 lastMousePosition = Vector2.Zero;

        /// <summary>
        /// Updates the camera and rotates it according to the mouse movment
        /// </summary>
        /// <param name="view">The viewport of the game</param>
        public override void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Forward)) MoveForwards();
            if (keyboardState.IsKeyDown(Backward)) MoveBackwards();
            if (keyboardState.IsKeyDown(Rigth)) MoveRigth();
            if (keyboardState.IsKeyDown(Left)) MoveLeft();

            Vector2 mousePotsition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Vector2 Center = new Vector2(Graphics.Viewport.Width / 2, Graphics.Viewport.Height / 2);

            rotation.X += (mousePotsition.Y - Center.Y) * Speed * 0.02f * MouseSensitivity;
            rotation.Y += (mousePotsition.X - Center.X) * Speed * 0.02f * MouseSensitivity;

            rotation.X = MathHelper.Clamp(rotation.X, -MathHelper.PiOver2, MathHelper.PiOver2);

            Mouse.SetPosition((int)Center.X, (int)Center.Y);

            lastMousePosition = mousePotsition;

            CreateView();
        }


        protected override void CreateProjection()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Graphics.Viewport.AspectRatio, 0.1f, 100000f);
        }
        protected override void CreateView()
        {
            Matrix RotationMatrix = Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationX(Rotation.X);
            //Vector3 TransformedPosition = Vector3.Transform(position, RotationMatrix);
            //Vector3 LookAt = new Vector3(TransformedPosition.X, TransformedPosition.Y, TransformedPosition.Z - 10);

            Vector3 transformedReference = Vector3.Transform(new Vector3(0, 0, 1), RotationMatrix);

            Vector3 cameraLookat = Position + transformedReference;

            View = Matrix.CreateLookAt(Position, cameraLookat, Vector3.UnitY);
        }

        #region MovingMethods
        private void MoveLeft()
        {
            position.Z += Speed * (float)Math.Cos(Rotation.Y + MathHelper.PiOver2);
            position.X -= Speed * (float)Math.Sin(Rotation.Y + MathHelper.PiOver2);
        }
        private void MoveRigth()
        {
            position.Z -= Speed * (float)Math.Cos(Rotation.Y + MathHelper.PiOver2);
            position.X += Speed * (float)Math.Sin(Rotation.Y + MathHelper.PiOver2);
        }
        private void MoveBackwards()
        {
            position.Z += Speed * (float)Math.Cos(Rotation.Y);
            position.X -= Speed * (float)Math.Sin(Rotation.Y);
        }
        private void MoveForwards()
        {
            position.Z -= Speed * (float)Math.Cos(Rotation.Y);
            position.X += Speed * (float)Math.Sin(Rotation.Y);
        }
        #endregion
    }
}
