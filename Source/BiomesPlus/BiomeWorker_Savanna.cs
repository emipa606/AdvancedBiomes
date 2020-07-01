using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000004 RID: 4
    public class BiomeWorker_Savanna : BiomeWorker
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002204 File Offset: 0x00000404
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
				result = 22.5f + (tile.temperature - 20f) * 1.8f + (tile.rainfall - 600f) / 85f;
			}
			result = result * (LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().Savanna / 100);
			return result;
		}
	}
}
