using System;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000012 RID: 18
	public class TerrainComp_HeatPush : TerrainComp
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public TerrainCompProperties_HeatPush Props
		{
			get
			{
				return (TerrainCompProperties_HeatPush)this.props;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D08 File Offset: 0x00000F08
		protected virtual bool ShouldPushHeat
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D1C File Offset: 0x00000F1C
		protected virtual float PushAmount
		{
			get
			{
				return this.Props.pushAmount;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D3C File Offset: 0x00000F3C
		public override void CompTick()
		{
			base.CompTick();
			bool flag = Find.TickManager.TicksGame % 60 == this.HashCodeToMod(60) && this.ShouldPushHeat;
			if (flag)
			{
				GenTemperature.PushHeat(this.parent.Position, this.parent.Map, this.PushAmount);
			}
		}
	}
}
