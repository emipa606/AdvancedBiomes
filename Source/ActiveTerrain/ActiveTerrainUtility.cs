using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ActiveTerrain;

[StaticConstructorOnStartup]
public static class ActiveTerrainUtility
{
    private static readonly Material NeedsPowerMat =
        MaterialPool.MatFrom("UI/Overlays/NeedsPower", ShaderDatabase.MetaOverlay);

    static ActiveTerrainUtility()
    {
        new Harmony("com.spdskatr.activeterrain.patches").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message(
            "Active Terrain Framework initialized. This mod uses Harmony (all patches are non-destructive): Verse.TerrainGrid.SetTerrain, Verse.TerrainGrid.RemoveTopLayer, Verse.MouseoverReadout.MouseoverReadoutOnGUI");
    }

    public static int HashCodeToMod(this object obj, int mod)
    {
        return Math.Abs(obj.GetHashCode()) % mod;
    }

    public static CompTempControl GetTempControl(this Room room, TempControlType targetType)
    {
        foreach (var c in room.Cells)
        {
            var firstBuilding = c.GetFirstBuilding(room.Map);
            if (firstBuilding == null || !firstBuilding.Powered())
            {
                continue;
            }

            var comp = firstBuilding.GetComp<CompTempControl>();
            if (comp == null)
            {
                continue;
            }

            if ((comp.AnalyzeType() & targetType) > TempControlType.None)
            {
                return comp;
            }
        }

        return null;
    }

    public static TempControlType AnalyzeType(this CompTempControl tempControl)
    {
        var energyPerSecond = tempControl.Props.energyPerSecond;
        return energyPerSecond > 0f ? TempControlType.Heater :
            energyPerSecond < 0f ? TempControlType.Cooler : TempControlType.None;
    }

    public static TempControlType AnalyzeType(this TerrainComp_TempControl tempControl)
    {
        var energyPerSecond = tempControl.Props.energyPerSecond;
        return energyPerSecond > 0f ? TempControlType.Heater :
            energyPerSecond < 0f ? TempControlType.Cooler : TempControlType.None;
    }

    public static bool Powered(this ThingWithComps t)
    {
        var comp = t.GetComp<CompPowerTrader>();
        return comp == null || comp.PowerOn;
    }

    public static bool Powered(this TerrainInstance t)
    {
        var comp = t.GetComp<TerrainComp_PowerTrader>();
        return comp == null || comp.PowerOn;
    }

    public static void RenderPulsingNeedsPowerOverlay(IntVec3 loc)
    {
        var vector = loc.ToVector3ShiftedWithAltitude(AltitudeLayer.MapDataOverlay);
        var num = (Time.realtimeSinceStartup + (397f * loc.HashCodeToMod(37))) * 4f;
        var num2 = ((float)Math.Sin(num) + 1f) * 0.5f;
        num2 = 0.3f + (num2 * 0.7f);
        var material = FadedMaterialPool.FadedVersionOf(NeedsPowerMat, num2);
        Graphics.DrawMesh(MeshPool.plane08, vector, Quaternion.identity, material, 0);
    }

    public static CompPowerTraderFloor TryFindNearestPowerConduitFloor(IntVec3 center, Map map)
    {
        var cellRect = CellRect.CenteredOn(center, 6);
        Building building = null;
        var num = float.MaxValue;
        for (var i = cellRect.minZ; i <= cellRect.maxZ; i++)
        {
            for (var j = cellRect.minX; j <= cellRect.maxX; j++)
            {
                var intVec = new IntVec3(j, 0, i);
                var transmitter = intVec.GetTransmitter(map);
                if (transmitter?.GetComp<CompPowerTraderFloor>() == null)
                {
                    continue;
                }

                var lengthHorizontalSquared = (intVec - center).LengthHorizontalSquared;
                if (!(num > lengthHorizontalSquared))
                {
                    continue;
                }

                building = transmitter;
                num = lengthHorizontalSquared;
            }
        }

        return building?.GetComp<CompPowerTraderFloor>();
    }

    public static TerrainInstance MakeTerrainInstance(this SpecialTerrain tDef, Map map, IntVec3 loc)
    {
        var terrainInstance = (TerrainInstance)Activator.CreateInstance(tDef.terrainInstanceClass);
        terrainInstance.def = tDef;
        terrainInstance.Map = map;
        terrainInstance.Position = loc;
        return terrainInstance;
    }
}