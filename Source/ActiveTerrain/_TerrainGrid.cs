using System.Reflection;
using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(TerrainGrid), "SetTerrain", null)]
    public static class _TerrainGrid
    {
        static _TerrainGrid()
        {
            new Harmony("com.spdskatr.activeterrain.patches").PatchAll(Assembly.GetExecutingAssembly());
            Log.Message(
                "Active Terrain Framework initialized. This mod uses Harmony (all patches are non-destructive): Verse.TerrainGrid.SetTerrain, Verse.TerrainGrid.RemoveTopLayer, Verse.MouseoverReadout.MouseoverReadoutOnGUI");
        }

        [HarmonyPriority(600)]
        private static void Prefix(IntVec3 c, TerrainDef newTerr, TerrainGrid __instance)
        {
            var value = Traverse.Create(__instance).Field("map").GetValue<Map>();
            var terrainDef = value.terrainGrid.TerrainAt(c);
            if (terrainDef is SpecialTerrain)
            {
                value.GetComponent<SpecialTerrainList>().Notify_RemovedTerrainAt(c);
            }
        }

        private static void Postfix(IntVec3 c, TerrainDef newTerr, TerrainGrid __instance)
        {
            SpecialTerrain special;
            if ((special = newTerr as SpecialTerrain) == null)
            {
                return;
            }

            var component = Traverse.Create(__instance).Field("map").GetValue<Map>()
                .GetComponent<SpecialTerrainList>();
            component.RegisterAt(special, c);
        }
    }
}
