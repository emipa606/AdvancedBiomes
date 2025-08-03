using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain;

public sealed class TerrainInstance : IExposable
{
    private readonly List<TerrainComp> comps = [];

    public SpecialTerrain def;

    private Map mapInt;

    private IntVec3 positionInt;

    public Map Map
    {
        get => mapInt;
        set => mapInt = value;
    }

    public IntVec3 Position
    {
        get => positionInt;
        set => positionInt = value;
    }

    public string Label
    {
        get
        {
            string text = def.LabelCap;
            foreach (var terrainComp in comps)
            {
                text = terrainComp.TransformLabel(text);
            }

            return text;
        }
    }

    public void ExposeData()
    {
        Scribe_References.Look(ref mapInt, "map");
        Scribe_Values.Look(ref positionInt, "pos");
        Scribe_Defs.Look(ref def, "def");
        if (Scribe.mode == LoadSaveMode.LoadingVars)
        {
            initializeComps();
        }

        foreach (var terrainComp in comps)
        {
            terrainComp.PostExposeData();
        }
    }

    public void Init()
    {
        initializeComps();
    }

    public T GetComp<T>() where T : TerrainComp
    {
        foreach (var terrainComp in comps)
        {
            T result;
            if ((result = terrainComp as T) != null)
            {
                return result;
            }
        }

        return null;
    }

    private void initializeComps()
    {
        foreach (var terrainCompProperties in def.terrainComps)
        {
            var terrainComp = (TerrainComp)Activator.CreateInstance(terrainCompProperties.compClass);
            terrainComp.parent = this;
            comps.Add(terrainComp);
            terrainComp.Initialize(terrainCompProperties);
        }
    }

    public void Tick()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.CompTick();
        }
    }

    public void Update()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.CompUpdate();
        }
    }

    public void PostPlacedDown()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PlaceSetup();
        }
    }

    public void PostRemove()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PostRemove();
        }
    }

    public void PostLoad()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PostPostLoad();
        }
    }

    public void BroadcastCompSignal(string sig)
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.ReceiveCompSignal(sig);
        }
    }
}