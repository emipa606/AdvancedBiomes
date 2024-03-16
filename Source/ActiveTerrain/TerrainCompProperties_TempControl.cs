namespace ActiveTerrain;

public class TerrainCompProperties_TempControl : TerrainCompProperties
{
    public readonly bool cleansSnow = true;

    public readonly float lowPowerConsumptionFactor = 0.2f;

    public readonly bool reliesOnPower = true;

    public readonly float snowMeltAmountPerSecond = 0.02f;

    public float energyPerSecond;

    public TerrainCompProperties_TempControl()
    {
        compClass = typeof(TerrainComp_TempControl);
    }
}