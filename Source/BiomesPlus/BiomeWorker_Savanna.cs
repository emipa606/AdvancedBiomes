using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus
{
    public class BiomeWorker_Savanna : BiomeWorker
    {
        public override float GetScore(Tile tile, int tileID)
        {
            float result;
            if (tile.WaterCovered)
            {
                result = -100f;
            }
            else if (tile.temperature < -10f)
            {
                result = 0f;
            }
            else if (tile.rainfall < 600f || tile.rainfall >= 2000f)
            {
                result = 0f;
            }
            else
            {
                result = 22.5f + ((tile.temperature - 20f) * 1.8f) + ((tile.rainfall - 600f) / 85f);
            }

            result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Savanna / 100;
            return result;
        }
    }
}
