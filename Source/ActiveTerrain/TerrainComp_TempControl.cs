using RimWorld;
using UnityEngine;
using Verse;

namespace ActiveTerrain;

public sealed class TerrainComp_TempControl : TerrainComp_HeatPush
{
    private bool operatingAtHighPower;

    [Unsaved] private CompTempControl parentTempControl;

    public TerrainCompProperties_TempControl Props => (TerrainCompProperties_TempControl)props;

    private float AmbientTemperature => GenTemperature.GetTemperatureForCell(parent.Position, parent.Map);

    private float PowerConsumptionNow
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

    private CompTempControl HeaterToConformTo
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

    private float TargetTemperature
    {
        get
        {
            var heaterToConformTo = HeaterToConformTo;
            return heaterToConformTo?.targetTemperature ?? 21f;
        }
    }

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

    private void CleanSnow()
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

    private void UpdatePowerConsumption()
    {
        var comp = parent.GetComp<TerrainComp_PowerTrader>();
        if (comp != null)
        {
            comp.PowerOutput = -PowerConsumptionNow;
        }
    }

    public override string TransformLabel(string label)
    {
        return $"{base.TransformLabel(label)} " + (operatingAtHighPower
            ? "HeatedFloor_HighPower".Translate()
            : "HeatedFloor_LowPower".Translate());
    }
}