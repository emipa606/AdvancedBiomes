using HarmonyLib;
using Verse;

namespace ActiveTerrain;

[HarmonyPatch(typeof(TerrainGrid), nameof(TerrainGrid.SetTerrain), null)]
public static class TerrainGrid_SetTerrain
{
    [HarmonyPriority(600)]
    private static void Prefix(IntVec3 c, Map ___map)
    {
        var terrainDef = ___map.terrainGrid.TerrainAt(c);
        if (terrainDef is SpecialTerrain)
        {
            ___map.GetComponent<SpecialTerrainList>().Notify_RemovedTerrainAt(c);
        }
    }

    private static void Postfix(IntVec3 c, TerrainDef newTerr, Map ___map)
    {
        SpecialTerrain special;
        if ((special = newTerr as SpecialTerrain) == null)
        {
            return;
        }

        var component = ___map.GetComponent<SpecialTerrainList>();
        component.RegisterAt(special, c);
    }
}