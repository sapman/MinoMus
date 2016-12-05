using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.TwoDimensional
{
    public class Camera
    {
        #region Fields

        protected float zoom;
        protected Matrix transform;
        protected Matrix inverseTransform;
        public Vector2 Center;
        protected float rotation;
        public Viewport view;
        private Vector2 origin;

        #endregion

        #region Properties

        public float Zoom
        {
            get { return zoom; }
            set { zoom = value; }
        }
        /// <summary>
        /// Camera View Matrix Property
        /// </summary>
        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }
        /// <summary>
        /// Inverse of the view matrix, can be used to get objects screen coordinates
        /// from its object coordinates
        /// </summary>
        public Matrix InverseTransform
        {
            get { return inverseTransform; }
        }
        public Vector2 Pos
        {
            get { return Center; }
            set { Center = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public bool FollowingX { get; set; }
        public bool FollowingY { get; set; }
        #endregion

        #region Constructor

        public Camera(Viewport viewport)
        {
            zoom = 0.5f;
            rotation = 0.0f;
            Center = Vector2.Zero;
            this.view = viewport;
            FollowingX = true;
            FollowingY = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update the camera view
        /// </summary>
        public void Update(Vector2 position)
        {
            Center = position;

            origin = new Vector2(view.Width / 2, view.Height / 2) / zoom;
            //Clamp zoom value
            zoom = MathHelper.Clamp(zoom, 0.0f, 10.0f);
            //Clamp rotation value
            rotation = ClampAngle(rotation);
            //Create view matrix
            if (FollowingX && FollowingY)
            {
                transform = Matrix.Identity *
                    Matrix.CreateTranslation(-(position.X), -(position.Y), 0) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new Vector3(origin, 0)) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1));
            }
            else if (FollowingY && !FollowingX)
            {
                transform = Matrix.Identity *
                    Matrix.CreateTranslation(-(view.Width / 2f), -(position.Y), 0) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new Vector3(origin, 0)) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1));
            }
            else // Following X && !FollowingY
            {
                transform = Matrix.Identity *
                    Matrix.CreateTranslation(-(position.X), -(view.Height / 2f), 0) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new Vector3(origin, 0)) *
                    Matrix.CreateScale(new Vector3(zoom, zoom, 1));
            }
            //Update inverse matrix
            inverseTransform = Matrix.Invert(transform);
        }

        /// <summary>
        /// Clamps a radian value between -pi and pi
        /// </summary>
        /// <param name="radians">angle to be clamped</param>
        /// <returns>clamped angle</returns>
        protected float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        #endregion
    }
}


    //public class Camera
    //{
    //    public Viewport view;
    //    public Vector2 Center;
    //    public Matrix Transform;

    //    private float zoom  = 1 ;
    //    public float Zoom { get { return zoom; } set { zoom = value; } }
    //    public float Rotation { get; set; }
    //    public bool CenterY { get; set; }
    //    public bool CenterX { get; set; }

    //    public Camera(Viewport newView)
    //    {
    //        view = newView;
    //        CenterX = true;
    //        CenterY = false;
    //    }

    //    public void Update(Vector2 objectPosition)
    //    {
    //        Center = new Vector2();
    //        if(CenterX)
    //            Center.X = objectPosition.X ;
    //        if(CenterY)
    //            Center.Y = objectPosition.Y ;

    //        ClampRotation();
    //        MathHelper.Clamp(zoom, 0f, 10f);

    //        Transform = Matrix.CreateRotationZ(Rotation) *              
    //            Matrix.CreateScale(new Vector3(zoom, zoom, 0))*
    //            Matrix.CreateTranslation(new Vector3(Center, 0));
                
    //    }


    //    private void ClampRotation()
    //    {
    //        while (Rotation > MathHelper.Pi)
    //        {
    //            Rotation -= MathHelper.TwoPi;
    //        }
    //        while (Rotation < -MathHelper.Pi)
    //        {
    //            Rotation += MathHelper.TwoPi ;
    //        }
    //    }
    //}
