using System;

namespace ActiveTerrain
{
	// Token: 0x02000010 RID: 16
	public class TerrainCompProperties_TempControl : TerrainCompProperties
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002C4B File Offset: 0x00000E4B
		public TerrainCompProperties_TempControl()
		{
			compClass = typeof(TerrainComp_TempControl);
		}

		// Token: 0x04000017 RID: 23
		public float energyPerSecond;

		// Token: 0x04000018 RID: 24
		public bool reliesOnPower = true;

		// Token: 0x04000019 RID: 25
		public float lowPowerConsumptionFactor = 0.2f;

		// Token: 0x0400001A RID: 26
		public bool cleansSnow = true;

		// Token: 0x0400001B RID: 27
		public float snowMeltAmountPerSecond = 0.02f;
	}
}
