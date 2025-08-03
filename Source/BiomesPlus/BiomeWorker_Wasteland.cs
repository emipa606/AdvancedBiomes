using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus;

public class BiomeWorker_Wasteland : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        float result;
        if (tile.WaterCovered)
        {
            result = -100f;
        }
        else if (tile.temperature < 0f || tile.rainfall < 200f)
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