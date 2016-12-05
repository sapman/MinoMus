using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MinoMus.ThreeDimentional
{
    /// <summary>
    /// Represents a instance of a model
    /// </summary>
    public class ModelInstance
    {
        public Model Model;

        protected Vector3 position;
        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Matrix world;
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        protected Vector3 rotation;
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        public bool ApplyTexture { get; set; }
        private Texture2D textureEffect;
        public Texture2D TextureEffect
        {
            get { return textureEffect; }
            set { textureEffect = value; ApplyTexture = true; }
        }
        


        public float Scale { get; set; }
        public float Speed { get; set; }

        bool ApplyLight { get; set; }
        LightSource light;
        public LightSource Light { get { return light; } set { light = value; ApplyLight = true; } }

        bool ApplyFog { get; set; }
        Fog fog;
        public Fog Fog { get { return fog; } set { fog = value; ApplyFog = true; } }

        protected ModelInstance()
        {
            position = new Vector3();
            rotation = new Vector3();
            world = Matrix.Identity;
            Scale = 1f;
        }

        public ModelInstance(Model model, Vector3 startingPosition, Vector3 startingRotation)
        {
            Model = model;
            position = startingPosition;
            rotation = startingRotation;

            Scale = 1f;

            CreateWorld();
        }

        protected void CreateWorld()
        {
            world = Matrix.CreateScale(Scale) *
                Matrix.CreateRotationY(Rotation.Y) *
                Matrix.CreateRotationX(Rotation.X) *
                Matrix.CreateRotationZ(Rotation.Z) *
                Matrix.CreateTranslation(Position);
            //World = Matrix.CreateTranslation(Vector3.Zero);
        }

        public virtual void Update()
        {
            CreateWorld();
        }

        public virtual void Draw(Camera3D Camera)
        {
            //Model.Draw(World, Camera.View, Camera.Projection);

            foreach (ModelMesh Mesh in Model.Meshes)
            {
                foreach (BasicEffect effect in Mesh.Effects)
                {
                    if (ApplyLight)
                        Light.ApplyLight(effect, position);

                    if (ApplyFog)
                        fog.ApplyFog(effect);

                    if (ApplyTexture)
                        effect.Texture = textureEffect;

                    effect.World = World;
                    effect.View = Camera.View;
                    effect.Projection = Camera.Projection;
                }
                Mesh.Draw();
            }

            //Model.Draw(world, Camera.View, Camera.Projection);
        }
        
    }
}
