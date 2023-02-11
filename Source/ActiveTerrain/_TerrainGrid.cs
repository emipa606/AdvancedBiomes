using System.Reflection;
using HarmonyLib;
using Verse;

namespace ActiveTerrain;

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