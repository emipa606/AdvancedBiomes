using System;
using RimWorld;
using RimWorld.Planet;

namespace BiomesPlus
{
	// Token: 0x02000006 RID: 6
	public class BiomeWorker_Volcano : BiomeWorker
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002354 File Offset: 0x00000554
		public override float GetScore(Tile tile, int tileID)
		{
			float result;
			if (tile.hilliness != Hilliness.Mountainous & tile.hilliness != Hilliness.Impassable)
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
			else
			{
				result = 22.5f + (tile.temperature - 20f) * 2.2f + (tile.rainfall - 600f) / 100f;
			}
			return result;
		}
	}
}
