using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional
{
    public class LightSource : ModelInstance
    {
        private Vector3 diffuseColor;
        public Vector3 DiffuseColor 
        {
            get { return diffuseColor ; }
            set { diffuseColor  = value; }
        }

        private Vector3 specularColor;
        public Vector3 SpecularColor
        {
            get { return specularColor; }
            set { specularColor = value; }
        }

        private Vector3 ambientLightColor;
        public Vector3 AmbientLightColor
        {
            get { return ambientLightColor; }
            set { ambientLightColor = value; }
        }

        private Vector3 emissiveColor;
        public Vector3 EmissiveColor
        {
            get { return emissiveColor; }
            set { emissiveColor = value; }
        }

        bool Basic = false;

        private LightSource()
        {
            // TODO: Complete member initialization
        }

        public static LightSource CreateBasicLightSource()
        {
            LightSource ls = new LightSource();
            ls.Basic = true;
            return ls;
        }

        public LightSource(Model Model, Vector3 SourcePosition, Vector3 DiffuseColour)
        {
            this.Model = Model;
            position = SourcePosition;
            diffuseColor = DiffuseColour;

            CreateWorld();
        }

        public LightSource(Model Model, Vector3 SourcePosition, Vector3 DiffuseColour , Vector3 SpecularColour, 
            Vector3 AmbientColour, Vector3 EmissiveColour)
        {
            this.Model = Model;
            position = SourcePosition;
            diffuseColor = DiffuseColour;
            specularColor = SpecularColour;
            ambientLightColor = AmbientColour;
            emissiveColor = EmissiveColour;

            CreateWorld();
        }


        public void ApplyLight(BasicEffect Effect, Vector3 MeshPosition)
        {
            CreateWorld();

            if (Basic)
            {
                Effect.EnableDefaultLighting();
                return;
            }

            Vector3 Direction = Position - MeshPosition;

            Effect.LightingEnabled = true; // Turn on the lighting subsystem.

            Effect.DirectionalLight0.DiffuseColor = DiffuseColor; // a reddish light
            Effect.DirectionalLight0.Direction = Direction;  // coming along the x-axis
            Effect.DirectionalLight0.SpecularColor = SpecularColor; // with green highlights

            Effect.AmbientLightColor = AmbientLightColor; // Add some overall ambient light.
            Effect.EmissiveColor = EmissiveColor; // Sets some strange emmissive lighting.  This just looks weird.
        }
        
    }
}
