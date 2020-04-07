using System;

namespace ActiveTerrain
{
	// Token: 0x02000017 RID: 23
	public class TerrainComp_HeatPushPowered : TerrainComp_HeatPush
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003938 File Offset: 0x00001B38
		protected override bool ShouldPushHeat
		{
			get
			{
				return this.parent.GetComp<TerrainComp_PowerTrader>() == null || this.parent.GetComp<TerrainComp_PowerTrader>().PowerOn;
			}
		}
	}
}
