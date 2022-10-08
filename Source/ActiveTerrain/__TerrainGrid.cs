using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
    [HarmonyPatch(typeof(TerrainGrid), "RemoveTopLayer", null)]
    internal static class __TerrainGrid
    {
        [HarmonyPriority(600)]
        private static void Prefix(IntVec3 c, TerrainGrid __instance)
        {
            if (__instance.TerrainAt(c) is not SpecialTerrain)
            {
                return;
            }

            var component = Traverse.Create(__instance).Field("map").GetValue<Map>()
                .GetComponent<SpecialTerrainList>();
            component.Notify_RemovedTerrainAt(c);
        }
    }
}
