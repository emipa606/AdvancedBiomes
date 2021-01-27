using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000005 RID: 5
    public class BiomeWorker_Wetland : BiomeWorker
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000022A8 File Offset: 0x000004A8
		public override float GetScore(Tile tile, int tileID)
		{
			float result;
			if (tile.hilliness != Hilliness.Flat)
			{
				result = -100f;
			}
			else if (tile.elevation <= 0f)
			{
				result = -100f;
			}
			else if (tile.elevation > 1000f)
			{
				result = 0f;
			}
			else if (tile.rainfall < 2000f)
			{
				result = 0f;
			}
			else
			{
				result = 16f + (tile.temperature - 7f) + ((tile.rainfall - 600f) / 155f);
			}
			result *= LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Wetland / 100;
			return result;
		}
	}
}
