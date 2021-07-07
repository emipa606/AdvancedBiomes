using RimWorld;
using UnityEngine;
using Verse;

namespace ActiveTerrain
{
    // Token: 0x02000014 RID: 20
    public class TerrainComp_TempControl : TerrainComp_HeatPush
    {
        // Token: 0x04000022 RID: 34
        public bool operatingAtHighPower;

        // Token: 0x04000023 RID: 35
        [Unsaved] private CompTempControl parentTempControl;

        // Token: 0x17000009 RID: 9
        // (get) Token: 0x0600004C RID: 76 RVA: 0x0000301C File Offset: 0x0000121C
        public new TerrainCompProperties_TempControl Props => (TerrainCompProperties_TempControl) props;

        // Token: 0x1700000A RID: 10
        // (get) Token: 0x0600004D RID: 77 RVA: 0x0000303C File Offset: 0x0000123C
        public float AmbientTemperature => GenTemperature.GetTemperatureForCell(parent.Position, parent.Map);

        // Token: 0x1700000B RID: 11
        // (get) Token: 0x0600004E RID: 78 RVA: 0x0000306C File Offset: 0x0000126C
        public float PowerConsumptionNow
        {
            get
            {
                var basePowerConsumption = parent.def.GetCompProperties<TerrainCompProperties_PowerTrader>()
                    .basePowerConsumption;
                return operatingAtHighPower
                    ? basePowerConsumption
                    : basePowerConsumption * Props.lowPowerConsumptionFactor;
            }
        }

        // Token: 0x1700000C RID: 12
        // (get) Token: 0x0600004F RID: 79 RVA: 0x000030AC File Offset: 0x000012AC
        public virtual CompTempControl HeaterToConformTo
        {
            get
            {
                CompTempControl result;
                if (parentTempControl != null && parentTempControl.parent.Spawned)
                {
                    parentTempControl = null;
                    result = parentTempControl;
                }
                else
                {
                    var room = parent.Position.GetRoom(parent.Map);
                    if (room == null)
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
        private float TargetTemperature
        {
            get
            {
                var heaterToConformTo = HeaterToConformTo;
                return heaterToConformTo?.targetTemperature ?? 21f;
            }
        }

        // Token: 0x1700000E RID: 14
        // (get) Token: 0x06000051 RID: 81 RVA: 0x00003158 File Offset: 0x00001358
        protected override float PushAmount
        {
            get
            {
                bool compPowerOn;
                if (Props.reliesOnPower)
                {
                    var comp = parent.GetComp<TerrainComp_PowerTrader>();
                    compPowerOn = comp == null || comp.PowerOn;
                }
                else
                {
                    compPowerOn = true;
                }

                float result;
                if (compPowerOn)
                {
                    var ambientTemperature = AmbientTemperature;
                    var num = ambientTemperature < 20f ? 1f :
                        ambientTemperature > 120f ? 0f : Mathf.InverseLerp(120f, 20f, ambientTemperature);
                    var energyLimit = Props.energyPerSecond * num * 4.16666651f;
                    var num2 = GenTemperature.ControlTemperatureTempChange(parent.Position, parent.Map, energyLimit,
                        TargetTemperature);
                    var comp2 = parent.GetComp<TerrainComp_PowerTrader>();
                    if (!Mathf.Approximately(num2, 0f) && parent.Position.GetRoom(parent.Map) != null)
                    {
                        GenTemperature.PushHeat(parent.Position, parent.Map, num2);
                    }

                    if (comp2 != null)
                    {
                        comp2.PowerOutput =
                            !Mathf.Approximately(num2, 0f) && parent.Position.GetRoom(parent.Map) != null
                                ? -comp2.Props.basePowerConsumption
                                : -comp2.Props.basePowerConsumption * Props.lowPowerConsumptionFactor;
                    }

                    operatingAtHighPower =
                        !Mathf.Approximately(num2, 0f) && parent.Position.GetRoom(parent.Map) != null;
                    result = !Mathf.Approximately(num2, 0f) && parent.Position.GetRoom(parent.Map) != null ? num2 : 0f;
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
            if (!Props.cleansSnow || Find.TickManager.TicksGame % 60 != this.HashCodeToMod(60))
            {
                return;
            }

            CleanSnow();
            UpdatePowerConsumption();
        }

        // Token: 0x06000053 RID: 83 RVA: 0x00003328 File Offset: 0x00001528
        protected virtual void CleanSnow()
        {
            var depth = parent.Map.snowGrid.GetDepth(parent.Position);
            if (Mathf.Approximately(0f, depth))
            {
                return;
            }

            operatingAtHighPower = true;
            var newDepth = Mathf.Max(depth - Props.snowMeltAmountPerSecond, 0f);
            parent.Map.snowGrid.SetDepth(parent.Position, newDepth);
        }

        // Token: 0x06000054 RID: 84 RVA: 0x000033AC File Offset: 0x000015AC
        private void UpdatePowerConsumption()
        {
            var comp = parent.GetComp<TerrainComp_PowerTrader>();
            if (comp != null)
            {
                comp.PowerOutput = -PowerConsumptionNow;
            }
        }

        // Token: 0x06000055 RID: 85 RVA: 0x000033E0 File Offset: 0x000015E0
        public override string TransformLabel(string label)
        {
            return base.TransformLabel(label) + " " + (operatingAtHighPower
                ? "HeatedFloor_HighPower".Translate()
                : "HeatedFloor_LowPower".Translate());
        }
    }
}