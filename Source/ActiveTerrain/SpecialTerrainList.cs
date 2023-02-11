using System.Collections.Generic;
using Verse;

namespace ActiveTerrain;

public class SpecialTerrainList : MapComponent
{
    public Dictionary<IntVec3, TerrainInstance> terrains = new Dictionary<IntVec3, TerrainInstance>();

    public SpecialTerrainList(Map map) : base(map)
    {
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Collections.Look(ref terrains, "terrains", LookMode.Value, LookMode.Deep);
    }

    public override void MapComponentTick()
    {
        base.MapComponentTick();
        foreach (var keyValuePair in terrains)
        {
            keyValuePair.Value.Tick();
        }
    }

    public override void FinalizeInit()
    {
        base.FinalizeInit();
        RefreshAllCurrentTerrain();
        CallPostLoad();
    }

    public void CallPostLoad()
    {
        foreach (var key in terrains.Keys)
        {
            terrains[key].PostLoad();
        }
    }

    public void RefreshAllCurrentTerrain()
    {
        foreach (var intVec in map)
        {
            var terrainDef = map.terrainGrid.TerrainAt(intVec);
            SpecialTerrain special;
            if ((special = terrainDef as SpecialTerrain) != null)
            {
                RegisterAt(special, intVec);
            }
        }
    }

    public void RegisterAt(SpecialTerrain special, int i)
    {
        RegisterAt(special, map.cellIndices.IndexToCell(i));
    }

    public void RegisterAt(SpecialTerrain special, IntVec3 cell)
    {
        if (terrains.ContainsKey(cell))
        {
            return;
        }

        var terrainInstance = special.MakeTerrainInstance(map, cell);
        terrainInstance.Init();
        terrains.Add(cell, terrainInstance);
    }

    public override void MapComponentUpdate()
    {
        base.MapComponentUpdate();
        foreach (var keyValuePair in terrains)
        {
            keyValuePair.Value.Update();
        }
    }

    public void Notify_RemovedTerrainAt(IntVec3 c)
    {
        var terrainInstance = terrains[c];
        terrains.Remove(c);
        terrainInstance.PostRemove();
    }
}