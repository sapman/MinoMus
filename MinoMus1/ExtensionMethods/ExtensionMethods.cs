using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MinoMus.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string GetMeshesNames(this Model model)
        {
            string st = "";
            int index = 0;
            foreach (var mesh in model.Meshes)
            {
                st += index + " - " + mesh.Name + "\n";
                index++;
            }
            return st;
        }
    }
}
