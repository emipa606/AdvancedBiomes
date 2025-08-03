using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus;

public class BiomeWorker_Wetland : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        float result;
        if (tile.hilliness != Hilliness.Flat)
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
                {
                    if (tile.rainfall < 2000f)
                    {
                        result = 0f;
                    }
                    else
                    {
                        result = 16f + (tile.temperature - 7f) + ((tile.rainfall - 600f) / 155f);
                    }

                    break;
                }
            }
        }

        result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Wetland / 100;
        return result;
    }
}