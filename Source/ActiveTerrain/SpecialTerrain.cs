using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain;

public class SpecialTerrain : TerrainDef
{
    public readonly List<TerrainCompProperties> terrainComps = new List<TerrainCompProperties>();

    public readonly Type terrainInstanceClass = typeof(TerrainInstance);

    public T GetCompProperties<T>() where T : TerrainCompProperties
    {
        foreach (var terrainCompProperties in terrainComps)
        {
            T result;
            if ((result = terrainCompProperties as T) != null)
            {
                return result;
            }
        }

        return default;
    }
}