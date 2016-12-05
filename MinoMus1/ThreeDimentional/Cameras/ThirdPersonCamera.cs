using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional.Cameras
{
    public class ThirdPersonCamera : Camera3D
    {
        private Vector3 offSet;
        public Vector3 OffSet
        {
            get { return offSet; }
            private set { offSet = value; }
        }

        public ThirdPersonCamera(Vector3 startingPosition, Vector3 startingRotation,Vector3 offset, GraphicsDevice graphics)
        {
            position = startingPosition;
            rotation = startingRotation;
            offSet = offset;

            Graphics = graphics;

            CreateProjection();
            CreateView();
        }

        public void Update(Vector3 ChasePostion, Vector3 ChaseRotation, float Speed,GameTime gameTime)
        {
            position = ChasePostion + Vector3.UnitZ * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds * (float)Math.Cos(Rotation.Y) +
                Vector3.UnitX * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds * (float)Math.Sin(Rotation.Y) +
                Vector3.UnitY * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation = ChaseRotation;

            CreateView();
        }

        public override void Update(Vector3 ChasePostion, Vector3 ChaseRotation)
        {
            position = ChasePostion;
            rotation = ChaseRotation;

            CreateView();
        }

        public void SetOffset(float dy, float angleInRadians)
        {
            offSet = new Vector3(0, dy, (float)Math.Cos(angleInRadians) * -dy);
        }
        public void SetOffset(float dx, float dy, float dz)
        {
            offSet = new Vector3(0, dy, dz);
        }
        public void SetOffset(Vector3 delta)
        {
            offSet = delta;
        }

        protected override void CreateView()
        {
            Matrix RotationMatrix = Matrix.CreateRotationY(Rotation.Y);
            Vector3 TransformedOffSet = Vector3.Transform(offSet, RotationMatrix);

            Vector3 cameraPosition = TransformedOffSet + position;
            Vector3 LookAt = new Vector3(TransformedOffSet.X, TransformedOffSet.Y, TransformedOffSet.Z - 10);
            View = Matrix.CreateLookAt(cameraPosition, position, Vector3.UnitY);
        }
    }
}
