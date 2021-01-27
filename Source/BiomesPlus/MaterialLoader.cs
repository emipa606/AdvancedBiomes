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
			select MaterialPool.MatFrom(tex)).ToList<Material>();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020F8 File Offset: 0x000002F8
		public static Material MatWithEnding(string dirPath, string ending)
		{
			Material material = (from mat in MaterialLoader.MatsFromTexturesInFolder(dirPath)
			where mat.mainTexture.name.ToLower().EndsWith(ending)
			select mat).FirstOrDefault<Material>();
			Material result;
			if (material == null)
			{
				Log.Warning("MatWithEnding: Dir " + dirPath + " lacks texture ending in " + ending, false);
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
