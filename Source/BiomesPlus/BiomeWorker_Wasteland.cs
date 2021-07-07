using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000007 RID: 7
    public class BiomeWorker_Wasteland : BiomeWorker
    {
        // Token: 0x0600000C RID: 12 RVA: 0x000023FC File Offset: 0x000005FC
        public override float GetScore(Tile tile, int tileID)
        {
            float result;
            if (tile.WaterCovered)
            {
                result = -100f;
            }
            else if (tile.temperature < 0f)
            {
                result = 0f;
            }
            else if (tile.rainfall < 200f)
            {
                result = 0f;
            }
            else
            {
                result = 12f;
            }

            result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Wasteland / 100;
            return result;
        }
    }
}