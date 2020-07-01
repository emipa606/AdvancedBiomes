using RimWorld;
using RimWorld.Planet;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000003 RID: 3
    public class BiomeWorker_PoisonForest : BiomeWorker
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000216C File Offset: 0x0000036C
		public override float GetScore(Tile tile, int tileID)
		{
			float result;
			if (tile.WaterCovered)
			{
				result = -100f;
			}
			else if (tile.temperature < 15f)
			{
				result = 0f;
			}
			else if (tile.rainfall < 2000f)
			{
				result = 0f;
			}
			else
			{
				result = 26f + (tile.temperature - 20f) * 2f + (tile.rainfall - 600f) / 200f;
			}
			result = result * (LoadedModManager.GetMod<BiomesPlusMod>().GetSettings<BiomesPlusSettings>().PoisonForest / 100);
			return result;
		}
	}
}
