namespace ActiveTerrain;

public class TerrainCompProperties_TempControl : TerrainCompProperties
{
    public bool cleansSnow = true;

    public float energyPerSecond;

    public float lowPowerConsumptionFactor = 0.2f;

    public bool reliesOnPower = true;

    public float snowMeltAmountPerSecond = 0.02f;

    public TerrainCompProperties_TempControl()
    {
        compClass = typeof(TerrainComp_TempControl);
    }
}