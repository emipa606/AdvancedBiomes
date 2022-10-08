using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace BiomesPlus
{
    public static class MaterialLoader
    {
        public static List<Material> MatsFromTexturesInFolder(string dirPath)
        {
            var text = "Textures/" + dirPath;
            return (from Texture2D tex in Resources.LoadAll(text, typeof(Texture2D))
                select MaterialPool.MatFrom(tex)).ToList();
        }

        public static Material MatWithEnding(string dirPath, string ending)
        {
            var material = (from mat in MatsFromTexturesInFolder(dirPath)
                where mat.mainTexture.name.ToLower().EndsWith(ending)
                select mat).FirstOrDefault();
            Material result;
            if (material == null)
            {
                Log.Warning("MatWithEnding: Dir " + dirPath + " lacks texture ending in " + ending);
                result = BaseContent.BadMat;
            }
            else
            {
                result = material;
            }

            return result;
        }
    }
}
