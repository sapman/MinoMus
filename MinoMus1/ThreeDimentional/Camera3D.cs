using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional
{
    public abstract class Camera3D
    {
        protected Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Matrix view;
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        protected GraphicsDevice Graphics;

        public Camera3D()
        {
            view = Matrix.Identity;
            projection = Matrix.Identity;
        }

        public virtual void Update()
        {
            CreateView();
        }
        public virtual void Update(Vector3 newPosition, Vector3 newRotation)
        {
            position = newPosition;
            rotation = newRotation;
            CreateView();
        }

        protected virtual void CreateProjection()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Graphics.Viewport.AspectRatio, 0.1f, 100000f);
        }
        protected virtual void CreateView()
        {
            Matrix RotationMatrix = Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationX(Rotation.X);
            Vector3 TransformedPosition = Vector3.Transform(position, RotationMatrix);
            Vector3 LookAt = new Vector3(TransformedPosition.X, TransformedPosition.Y, TransformedPosition.Z - 10);
            View = RotationMatrix *
                Matrix.CreateLookAt(TransformedPosition, LookAt, Vector3.UnitY);

        }
        
    }
}
