using Verse;

namespace ActiveTerrain
{
    // Token: 0x02000011 RID: 17
    public class TerrainCompProperties_Glower : TerrainCompProperties
    {
        // Token: 0x0400001E RID: 30
        public ColorInt glowColor = new ColorInt(255, 255, 255, 0) * 1.45f;

        // Token: 0x0400001D RID: 29
        public float glowRadius = 14f;

        // Token: 0x0400001C RID: 28
        public float overlightRadius;

        // Token: 0x0400001F RID: 31
        public bool powered = true;

        // Token: 0x0600003D RID: 61 RVA: 0x00002C8C File Offset: 0x00000E8C
        public TerrainCompProperties_Glower()
        {
            compClass = typeof(TerrainComp_Glower);
        }
    }
}