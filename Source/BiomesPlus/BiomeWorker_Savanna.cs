using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus;

public class BiomeWorker_Savanna : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        float result;
        if (tile.WaterCovered)
        {
            result = -100f;
        }
        else if (tile.temperature < -10f || tile.rainfall is < 600f or >= 2000f)
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