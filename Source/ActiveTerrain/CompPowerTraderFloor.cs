using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000019 RID: 25
	public class CompPowerTraderFloor : CompPowerTrader
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000039A7 File Offset: 0x00001BA7
		public override void SetUpPowerVars()
		{
			base.SetUpPowerVars();
			this.UpdatePowerOutput();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039B8 File Offset: 0x00001BB8
		public virtual void ReceiveTerrainComp(TerrainComp_PowerTrader comp)
		{
			this.acceptedComps.Add(comp);
			this.UpdatePowerOutput();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039CF File Offset: 0x00001BCF
		public virtual void Notify_TerrainCompRemoved(TerrainComp_PowerTrader comp)
		{
			this.acceptedComps.Remove(comp);
			this.UpdatePowerOutput();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000039E8 File Offset: 0x00001BE8
		public void UpdatePowerOutput()
		{
			float curPowerDemand = this.CurPowerDemand;
			float powerOutput = -base.Props.basePowerConsumption + curPowerDemand;
			base.PowerOutput = powerOutput;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003A14 File Offset: 0x00001C14
		public float CurPowerDemand
		{
			get
			{
				float num = 0f;
				foreach (TerrainComp_PowerTrader terrainComp_PowerTrader in this.acceptedComps)
				{
					num += terrainComp_PowerTrader.PowerOutput;
				}
				return this.cachedCurPowerDemand = num;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003A84 File Offset: 0x00001C84
		public override string CompInspectStringExtra()
		{
			return base.CompInspectStringExtra() + "FloorWire_InspectStringPart".Translate(new object[]
			{
				this.acceptedComps.Count,
				-this.cachedCurPowerDemand
			});
		}

		// Token: 0x0400002D RID: 45
		public List<TerrainComp_PowerTrader> acceptedComps = new List<TerrainComp_PowerTrader>();

		// Token: 0x0400002E RID: 46
		private float cachedCurPowerDemand;
	}
}
