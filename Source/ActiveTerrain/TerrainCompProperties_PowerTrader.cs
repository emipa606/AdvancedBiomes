namespace ActiveTerrain;

public class TerrainCompProperties_PowerTrader : TerrainCompProperties
{
    public float basePowerConsumption;

    public bool ignoreNeedsPower;

    public TerrainCompProperties_PowerTrader()
    {
        compClass = typeof(TerrainComp_PowerTrader);
    }
}