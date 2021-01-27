using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000008 RID: 8
	public class SpecialTerrain : TerrainDef
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002510 File Offset: 0x00000710
		public T GetCompProperties<T>() where T : TerrainCompProperties
		{
			for (var i = 0; i < terrainComps.Count; i++)
			{
				T result;
				var flag = (result = terrainComps[i] as T) != null;
				if (flag)
				{
					return result;
				}
			}
			return default;
		}

		// Token: 0x04000009 RID: 9
		public List<TerrainCompProperties> terrainComps = new List<TerrainCompProperties>();

		// Token: 0x0400000A RID: 10
		public Type terrainInstanceClass = typeof(TerrainInstance);
	}
}
