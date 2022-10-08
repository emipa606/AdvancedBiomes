namespace ActiveTerrain
{
    public class TerrainComp_SelfCleanPowered : TerrainComp_SelfClean
    {
        protected override bool CanClean => parent.GetComp<TerrainComp_PowerTrader>() == null ||
                                            parent.GetComp<TerrainComp_PowerTrader>().PowerOn;
    }
}
