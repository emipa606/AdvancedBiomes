using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000002 RID: 2
    public static class MaterialLoader
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002068 File Offset: 0x00000268
        public static List<Material> MatsFromTexturesInFolder(string dirPath)
        {
            var text = "Textures/" + dirPath;
            return (from Texture2D tex in Resources.LoadAll(text, typeof(Texture2D))
                select MaterialPool.MatFrom(tex)).ToList();
        }

        // Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
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