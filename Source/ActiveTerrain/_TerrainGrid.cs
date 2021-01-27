using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000005 RID: 5
	[StaticConstructorOnStartup]
	[HarmonyPatch(typeof(TerrainGrid), "SetTerrain", null)]
	public static class _TerrainGrid
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002383 File Offset: 0x00000583
		static _TerrainGrid()
		{
			new Harmony("com.spdskatr.activeterrain.patches").PatchAll(Assembly.GetExecutingAssembly());
			Log.Message("Active Terrain Framework initialized. This mod uses Harmony (all patches are non-destructive): Verse.TerrainGrid.SetTerrain, Verse.TerrainGrid.RemoveTopLayer, Verse.MouseoverReadout.MouseoverReadoutOnGUI", false);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023A8 File Offset: 0x000005A8
		[HarmonyPriority(600)]
		private static void Prefix(IntVec3 c, TerrainDef newTerr, TerrainGrid __instance)
		{
			Map value = Traverse.Create(__instance).Field("map").GetValue<Map>();
			TerrainDef terrainDef = value.terrainGrid.TerrainAt(c);
			var flag = terrainDef is SpecialTerrain;
			if (flag)
			{
				value.GetComponent<SpecialTerrainList>().Notify_RemovedTerrainAt(c);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023F8 File Offset: 0x000005F8
		private static void Postfix(IntVec3 c, TerrainDef newTerr, TerrainGrid __instance)
		{
			SpecialTerrain special;
			var flag = (special = newTerr as SpecialTerrain) != null;
			if (flag)
			{
				SpecialTerrainList component = Traverse.Create(__instance).Field("map").GetValue<Map>().GetComponent<SpecialTerrainList>();
				component.RegisterAt(special, c);
			}
		}
	}
}
