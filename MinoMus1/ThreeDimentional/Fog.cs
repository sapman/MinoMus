using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ThreeDimentional
{
    public class Fog
    {
        Vector3 Color;
        float FogStart;
        float FogEnd;

        public Fog(Color Color, float start, float end)
        {
            this.Color = Color.ToVector3();
            FogStart = start;
            FogEnd = end;
        }

        public void ApplyFog(BasicEffect Effect)
        {
            Effect.FogEnabled = true;
            Effect.FogStart = FogStart;
            Effect.FogEnd = FogEnd;
            Effect.FogColor = Color;
        }
    }
}
