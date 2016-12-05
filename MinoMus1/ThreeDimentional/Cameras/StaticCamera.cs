using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional.Cameras
{
    public class StaticCamera : Camera3D
    {
        public StaticCamera(Vector3 startingPosition, Vector3 startingRotation, GraphicsDevice graphics)
        {
            this.position = startingPosition;
            this.rotation = startingRotation;
            Graphics = graphics;

            CreateProjection();
            CreateView();
        }

        

        protected override void CreateView()
        {
            //Matrix RotationMatrix = Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationX(Rotation.X);
            //Vector3 TransformedPosition = Vector3.Transform(position, RotationMatrix);
            //Vector3 LookAt = new Vector3(TransformedPosition.X, TransformedPosition.Y, TransformedPosition.Z - 10);
            //View = RotationMatrix *
            //    Matrix.CreateLookAt(TransformedPosition, LookAt, Vector3.UnitY);

            ////Vector3 LookAt = new Vector3(Position.X, Position.Y, Position.Z - 10);
            ////View = Matrix.CreateLookAt(Position, LookAt, Vector3.UnitY);

            Matrix RotationMatrix = Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationX(Rotation.X);

            Vector3 transformedReference = Vector3.Transform(new Vector3(0, 0, 1), RotationMatrix);

            Vector3 cameraLookat = Position + transformedReference;

            View = Matrix.CreateLookAt(Position, cameraLookat, Vector3.UnitY);
        }

        protected override void CreateProjection()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Graphics.Viewport.AspectRatio, 0.1f, 100000f);
        }

        public void Update(Vector3 newPosition , Vector3 newRotation)
        {
            position = newPosition;
            rotation = newRotation;
            CreateView();
        }
    }
}

