using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus;

public class BiomeWorker_PoisonForest : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        float result;
        if (tile.WaterCovered)
        {
            result = -100f;
        }
        else if (tile.temperature < 15f || tile.rainfall < 2000f)
        {
            result = 0f;
        }
        else
        {
            result = 26f + ((tile.temperature - 20f) * 2f) + ((tile.rainfall - 600f) / 200f);
        }

        result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().PoisonForest / 100;
        return result;
    }
}