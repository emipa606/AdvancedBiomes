using System;

namespace ActiveTerrain
{
	// Token: 0x0200000D RID: 13
	public class TerrainCompProperties_HeatPush : TerrainCompProperties
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002BF2 File Offset: 0x00000DF2
		public TerrainCompProperties_HeatPush()
		{
			this.compClass = typeof(TerrainComp_HeatPush);
		}

		// Token: 0x04000013 RID: 19
		public float pushAmount;

		// Token: 0x04000014 RID: 20
		public int targetTemp = 5000;
	}
}
