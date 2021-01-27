using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x0200000A RID: 10
	public class SpecialTerrainList : MapComponent
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00002933 File Offset: 0x00000B33
		public SpecialTerrainList(Map map) : base(map)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002949 File Offset: 0x00000B49
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Collections.Look<IntVec3, TerrainInstance>(ref terrains, "terrains", LookMode.Value, LookMode.Deep);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002968 File Offset: 0x00000B68
		public override void MapComponentTick()
		{
			base.MapComponentTick();
			foreach (KeyValuePair<IntVec3, TerrainInstance> keyValuePair in terrains)
			{
				keyValuePair.Value.Tick();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029D0 File Offset: 0x00000BD0
		public override void FinalizeInit()
		{
			base.FinalizeInit();
			RefreshAllCurrentTerrain();
			CallPostLoad();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029E8 File Offset: 0x00000BE8
		public void CallPostLoad()
		{
			foreach (IntVec3 key in terrains.Keys)
			{
				terrains[key].PostLoad();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A50 File Offset: 0x00000C50
		public void RefreshAllCurrentTerrain()
		{
			foreach (IntVec3 intVec in map)
			{
				TerrainDef terrainDef = map.terrainGrid.TerrainAt(intVec);
				SpecialTerrain special;
				var flag = (special = terrainDef as SpecialTerrain) != null;
				if (flag)
				{
					RegisterAt(special, intVec);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void RegisterAt(SpecialTerrain special, int i)
		{
			RegisterAt(special, map.cellIndices.IndexToCell(i));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void RegisterAt(SpecialTerrain special, IntVec3 cell)
		{
			var flag = !terrains.ContainsKey(cell);
			if (flag)
			{
				TerrainInstance terrainInstance = special.MakeTerrainInstance(map, cell);
				terrainInstance.Init();
				terrains.Add(cell, terrainInstance);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B30 File Offset: 0x00000D30
		public override void MapComponentUpdate()
		{
			base.MapComponentUpdate();
			foreach (KeyValuePair<IntVec3, TerrainInstance> keyValuePair in terrains)
			{
				keyValuePair.Value.Update();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002B98 File Offset: 0x00000D98
		public void Notify_RemovedTerrainAt(IntVec3 c)
		{
			TerrainInstance terrainInstance = terrains[c];
			terrains.Remove(c);
			terrainInstance.PostRemove();
		}

		// Token: 0x0400000F RID: 15
		public Dictionary<IntVec3, TerrainInstance> terrains = new Dictionary<IntVec3, TerrainInstance>();
	}
}
