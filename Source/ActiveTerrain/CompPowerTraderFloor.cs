using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ActiveTerrain
{
    public class CompPowerTraderFloor : CompPowerTrader
    {
        public readonly List<TerrainComp_PowerTrader> acceptedComps = new List<TerrainComp_PowerTrader>();

        private float cachedCurPowerDemand;

        public float CurPowerDemand
        {
            get
            {
                var num = 0f;
                foreach (var terrainComp_PowerTrader in acceptedComps)
                {
                    num += terrainComp_PowerTrader.PowerOutput;
                }

                return cachedCurPowerDemand = num;
            }
        }

        public override void SetUpPowerVars()
        {
            base.SetUpPowerVars();
            UpdatePowerOutput();
        }

        public virtual void ReceiveTerrainComp(TerrainComp_PowerTrader comp)
        {
            acceptedComps.Add(comp);
            UpdatePowerOutput();
        }

        public virtual void Notify_TerrainCompRemoved(TerrainComp_PowerTrader comp)
        {
            acceptedComps.Remove(comp);
            UpdatePowerOutput();
        }

        public void UpdatePowerOutput()
        {
            var curPowerDemand = CurPowerDemand;
            var powerOutput = -Props.basePowerConsumption + curPowerDemand;
            PowerOutput = powerOutput;
        }

        public override string CompInspectStringExtra()
        {
            return base.CompInspectStringExtra() +
                   "FloorWire_InspectStringPart".Translate(acceptedComps.Count, -cachedCurPowerDemand);
        }
    }
}
