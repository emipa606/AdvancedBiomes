using Verse;

namespace ActiveTerrain;

public class TerrainCompProperties_Glower : TerrainCompProperties
{
    public readonly float glowRadius = 14f;

    public readonly bool powered = true;
    public ColorInt glowColor = new ColorInt(255, 255, 255, 0) * 1.45f;

    public float overlightRadius;

    public TerrainCompProperties_Glower()
    {
        compClass = typeof(TerrainComp_Glower);
    }
}