using System;
using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(TerrainGrid), "RemoveTopLayer", null)]
	internal static class __TerrainGrid
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000243C File Offset: 0x0000063C
		[HarmonyPriority(600)]
		private static void Prefix(IntVec3 c, TerrainGrid __instance)
		{
			bool flag = __instance.TerrainAt(c) is SpecialTerrain;
			if (flag)
			{
				SpecialTerrainList component = Traverse.Create(__instance).Field("map").GetValue<Map>().GetComponent<SpecialTerrainList>();
				component.Notify_RemovedTerrainAt(c);
			}
		}
	}
}
