using Verse;

namespace ActiveTerrain
{
    public class TerrainCompProperties_Glower : TerrainCompProperties
    {
        public ColorInt glowColor = new ColorInt(255, 255, 255, 0) * 1.45f;

        public float glowRadius = 14f;

        public float overlightRadius;

        public bool powered = true;

        public TerrainCompProperties_Glower()
        {
            compClass = typeof(TerrainComp_Glower);
        }
    }
}
