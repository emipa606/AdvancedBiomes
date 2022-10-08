namespace ActiveTerrain
{
    public class TerrainComp_HeatPushPowered : TerrainComp_HeatPush
    {
        protected override bool ShouldPushHeat => parent.GetComp<TerrainComp_PowerTrader>() == null ||
                                                  parent.GetComp<TerrainComp_PowerTrader>().PowerOn;
    }
}
