namespace ActiveTerrain;

public class TerrainCompProperties_HeatPush : TerrainCompProperties
{
    public float pushAmount;

    public int targetTemp = 5000;

    public TerrainCompProperties_HeatPush()
    {
        compClass = typeof(TerrainComp_HeatPush);
    }
}