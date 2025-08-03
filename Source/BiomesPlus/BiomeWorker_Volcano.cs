using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus;

public class BiomeWorker_Volcano : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        float result;
        if ((tile.hilliness != Hilliness.Mountainous) & (tile.hilliness != Hilliness.Impassable))
        {
            result = -100f;
        }
        else
        {
            switch (tile.elevation)
            {
                case <= 0f:
                    result = -100f;
                    break;
                case > 1000f:
                    result = 0f;
                    break;
                default:
                    result = 22.5f + ((tile.temperature - 20f) * 2.2f) + ((tile.rainfall - 600f) / 100f);
                    break;
            }
        }

        if (LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().VolcanoVariety &&
            result == -100f && tile.elevation > 750f && tile.hilliness > Hilliness.Flat)
        {
            result = 22.5f + ((tile.temperature - 20f) * 2.2f) + ((tile.rainfall - 600f) / 100f);
        }

        result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Volcano / 100;
        return result;
    }
}