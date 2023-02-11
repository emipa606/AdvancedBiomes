using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain;

public class TerrainInstance : IExposable
{
    public List<TerrainComp> comps = new List<TerrainComp>();

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

    public virtual string Label
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

    public virtual void ExposeData()
    {
        Scribe_References.Look(ref mapInt, "map");
        Scribe_Values.Look(ref positionInt, "pos");
        Scribe_Defs.Look(ref def, "def");
        if (Scribe.mode == LoadSaveMode.LoadingVars)
        {
            InitializeComps();
        }

        foreach (var terrainComp in comps)
        {
            terrainComp.PostExposeData();
        }
    }

    public virtual void Init()
    {
        InitializeComps();
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

        return default;
    }

    public void InitializeComps()
    {
        foreach (var terrainCompProperties in def.terrainComps)
        {
            var terrainComp = (TerrainComp)Activator.CreateInstance(terrainCompProperties.compClass);
            terrainComp.parent = this;
            comps.Add(terrainComp);
            terrainComp.Initialize(terrainCompProperties);
        }
    }

    public virtual void Tick()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.CompTick();
        }
    }

    public virtual void Update()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.CompUpdate();
        }
    }

    public virtual void PostPlacedDown()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PlaceSetup();
        }
    }

    public virtual void PostRemove()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PostRemove();
        }
    }

    public virtual void PostLoad()
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.PostPostLoad();
        }
    }

    public virtual void BroadcastCompSignal(string sig)
    {
        foreach (var terrainComp in comps)
        {
            terrainComp.ReceiveCompSignal(sig);
        }
    }
}