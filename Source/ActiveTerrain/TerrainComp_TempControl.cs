using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000014 RID: 20
	public class TerrainComp_TempControl : TerrainComp_HeatPush
	{
        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600004C RID: 76 RVA: 0x0000301C File Offset: 0x0000121C
        public new TerrainCompProperties_TempControl Props => (TerrainCompProperties_TempControl)props;

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600004D RID: 77 RVA: 0x0000303C File Offset: 0x0000123C
        public float AmbientTemperature => GenTemperature.GetTemperatureForCell(parent.Position, parent.Map);

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x0600004E RID: 78 RVA: 0x0000306C File Offset: 0x0000126C
        public float PowerConsumptionNow
		{
			get
			{
				var basePowerConsumption = parent.def.GetCompProperties<TerrainCompProperties_PowerTrader>().basePowerConsumption;
				return operatingAtHighPower ? basePowerConsumption : (basePowerConsumption * Props.lowPowerConsumptionFactor);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000030AC File Offset: 0x000012AC
		public virtual CompTempControl HeaterToConformTo
		{
			get
			{
				var flag = parentTempControl != null && parentTempControl.parent.Spawned;
				CompTempControl result;
				if (flag)
				{
					parentTempControl = null;
					result = parentTempControl;
				}
				else
				{
					Room room = parent.Position.GetRoom(parent.Map, RegionType.Set_Passable);
					var flag2 = room == null;
					if (flag2)
					{
						result = null;
					}
					else
					{
						result = parentTempControl = room.GetTempControl(this.AnalyzeType());
					}
				}
				return result;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003130 File Offset: 0x00001330
		public float TargetTemperature
		{
			get
			{
				CompTempControl heaterToConformTo = HeaterToConformTo;
				return (heaterToConformTo != null) ? heaterToConformTo.targetTemperature : 21f;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003158 File Offset: 0x00001358
		protected override float PushAmount
		{
			get
			{
				bool flag;
				if (Props.reliesOnPower)
				{
					TerrainComp_PowerTrader comp = parent.GetComp<TerrainComp_PowerTrader>();
					flag = comp == null || comp.PowerOn;
				}
				else
				{
					flag = true;
				}
				var flag2 = flag;
				float result;
				if (flag2)
				{
					var ambientTemperature = AmbientTemperature;
					var num = (ambientTemperature < 20f) ? 1f : ((ambientTemperature > 120f) ? 0f : Mathf.InverseLerp(120f, 20f, ambientTemperature));
					var energyLimit = Props.energyPerSecond * num * 4.16666651f;
					var num2 = GenTemperature.ControlTemperatureTempChange(parent.Position, parent.Map, energyLimit, TargetTemperature);
					var flag3 = !Mathf.Approximately(num2, 0f) && parent.Position.GetRoomGroup(parent.Map) != null;
					TerrainComp_PowerTrader comp2 = parent.GetComp<TerrainComp_PowerTrader>();
					var flag4 = flag3;
					if (flag4)
					{
						GenTemperature.PushHeat(parent.Position, parent.Map, num2);
					}
					var flag5 = comp2 != null;
					if (flag5)
					{
						comp2.PowerOutput = flag3 ? (-comp2.Props.basePowerConsumption) : (-comp2.Props.basePowerConsumption * Props.lowPowerConsumptionFactor);
					}
					operatingAtHighPower = flag3;
					result = flag3 ? num2 : 0f;
				}
				else
				{
					operatingAtHighPower = false;
					result = 0f;
				}
				return result;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000032D8 File Offset: 0x000014D8
		public override void CompTick()
		{
			base.CompTick();
			var flag = Props.cleansSnow && Find.TickManager.TicksGame % 60 == this.HashCodeToMod(60);
			if (flag)
			{
				CleanSnow();
				UpdatePowerConsumption();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003328 File Offset: 0x00001528
		public virtual void CleanSnow()
		{
			var depth = parent.Map.snowGrid.GetDepth(parent.Position);
			var flag = !Mathf.Approximately(0f, depth);
			if (flag)
			{
				operatingAtHighPower = true;
				var newDepth = Mathf.Max(depth - Props.snowMeltAmountPerSecond, 0f);
				parent.Map.snowGrid.SetDepth(parent.Position, newDepth);
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000033AC File Offset: 0x000015AC
		public void UpdatePowerConsumption()
		{
			TerrainComp_PowerTrader comp = parent.GetComp<TerrainComp_PowerTrader>();
			var flag = comp != null;
			if (flag)
			{
				comp.PowerOutput = -PowerConsumptionNow;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000033E0 File Offset: 0x000015E0
		public override string TransformLabel(string label)
		{
			return base.TransformLabel(label) + " " + (operatingAtHighPower ? Translator.Translate("HeatedFloor_HighPower") : Translator.Translate("HeatedFloor_LowPower"));
		}

		// Token: 0x04000022 RID: 34
		public bool operatingAtHighPower;

		// Token: 0x04000023 RID: 35
		[Unsaved]
		private CompTempControl parentTempControl;
	}
}
