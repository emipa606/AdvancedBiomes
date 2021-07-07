namespace ActiveTerrain
{
    // Token: 0x0200000F RID: 15
    public class TerrainCompProperties_PowerTrader : TerrainCompProperties
    {
        // Token: 0x04000015 RID: 21
        public float basePowerConsumption;

        // Token: 0x04000016 RID: 22
        public bool ignoreNeedsPower;

        // Token: 0x0600003B RID: 59 RVA: 0x00002C31 File Offset: 0x00000E31
        public TerrainCompProperties_PowerTrader()
        {
            compClass = typeof(TerrainComp_PowerTrader);
        }
    }
}