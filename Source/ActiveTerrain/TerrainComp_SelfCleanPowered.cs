using System;

namespace ActiveTerrain
{
	// Token: 0x02000018 RID: 24
	public class TerrainComp_SelfCleanPowered : TerrainComp_SelfClean
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000396C File Offset: 0x00001B6C
		protected override bool CanClean
		{
			get
			{
				return this.parent.GetComp<TerrainComp_PowerTrader>() == null || this.parent.GetComp<TerrainComp_PowerTrader>().PowerOn;
			}
		}
	}
}
