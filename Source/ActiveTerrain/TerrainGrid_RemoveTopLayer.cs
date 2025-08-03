using HarmonyLib;
using Verse;

namespace ActiveTerrain;

[HarmonyPatch(typeof(TerrainGrid), nameof(TerrainGrid.RemoveTopLayer), null)]
internal static class TerrainGrid_RemoveTopLayer
{
    [HarmonyPriority(600)]
    private static void Prefix(IntVec3 c, TerrainGrid __instance, Map ___map)
    {
        if (__instance.TerrainAt(c) is not SpecialTerrain)
        {
            return;
        }

        var component = ___map.GetComponent<SpecialTerrainList>();
        component.Notify_RemovedTerrainAt(c);
    }
}