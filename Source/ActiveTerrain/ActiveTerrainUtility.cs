using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000004 RID: 4
	[StaticConstructorOnStartup]
	public static class ActiveTerrainUtility
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public static int HashCodeToMod(this object obj, int mod)
		{
			return Math.Abs(obj.GetHashCode()) % mod;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A0 File Offset: 0x000002A0
		public static CompTempControl GetTempControl(this Room room, TempControlType targetType)
		{
			foreach (IntVec3 c in room.Cells)
			{
				Building firstBuilding = c.GetFirstBuilding(room.Map);
				bool flag = firstBuilding != null && firstBuilding.Powered();
				if (flag)
				{
					CompTempControl comp = firstBuilding.GetComp<CompTempControl>();
					bool flag2 = comp != null;
					if (flag2)
					{
						bool flag3 = (comp.AnalyzeType() & targetType) > TempControlType.None;
						if (flag3)
						{
							return comp;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002140 File Offset: 0x00000340
		public static TempControlType AnalyzeType(this CompTempControl tempControl)
		{
			float energyPerSecond = tempControl.Props.energyPerSecond;
			return (energyPerSecond > 0f) ? TempControlType.Heater : ((energyPerSecond < 0f) ? TempControlType.Cooler : TempControlType.None);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002178 File Offset: 0x00000378
		public static TempControlType AnalyzeType(this TerrainComp_TempControl tempControl)
		{
			float energyPerSecond = tempControl.Props.energyPerSecond;
			return (energyPerSecond > 0f) ? TempControlType.Heater : ((energyPerSecond < 0f) ? TempControlType.Cooler : TempControlType.None);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021B0 File Offset: 0x000003B0
		public static bool Powered(this ThingWithComps t)
		{
			CompPowerTrader comp = t.GetComp<CompPowerTrader>();
			return comp == null || comp.PowerOn;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D4 File Offset: 0x000003D4
		public static bool Powered(this TerrainInstance t)
		{
			TerrainComp_PowerTrader comp = t.GetComp<TerrainComp_PowerTrader>();
			return comp == null || comp.PowerOn;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F8 File Offset: 0x000003F8
		public static void RenderPulsingNeedsPowerOverlay(IntVec3 loc)
		{
			Vector3 vector = loc.ToVector3ShiftedWithAltitude(AltitudeLayer.MapDataOverlay);
			float num = (Time.realtimeSinceStartup + 397f * (float)loc.HashCodeToMod(37)) * 4f;
			float num2 = ((float)Math.Sin((double)num) + 1f) * 0.5f;
			num2 = 0.3f + num2 * 0.7f;
			Material material = FadedMaterialPool.FadedVersionOf(ActiveTerrainUtility.NeedsPowerMat, num2);
			Graphics.DrawMesh(MeshPool.plane08, vector, Quaternion.identity, material, 0);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002274 File Offset: 0x00000474
		public static CompPowerTraderFloor TryFindNearestPowerConduitFloor(IntVec3 center, Map map)
		{
			CellRect cellRect = CellRect.CenteredOn(center, 6);
			Building building = null;
			float num = float.MaxValue;
			for (int i = cellRect.minZ; i <= cellRect.maxZ; i++)
			{
				for (int j = cellRect.minX; j <= cellRect.maxX; j++)
				{
					IntVec3 intVec = new IntVec3(j, 0, i);
					Building transmitter = intVec.GetTransmitter(map);
					bool flag = transmitter != null && transmitter.GetComp<CompPowerTraderFloor>() != null;
					if (flag)
					{
						int lengthHorizontalSquared = (intVec - center).LengthHorizontalSquared;
						bool flag2 = num > (float)lengthHorizontalSquared;
						if (flag2)
						{
							building = transmitter;
							num = (float)lengthHorizontalSquared;
						}
					}
				}
			}
			return (building != null) ? building.GetComp<CompPowerTraderFloor>() : null;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002348 File Offset: 0x00000548
		public static TerrainInstance MakeTerrainInstance(this SpecialTerrain tDef, Map map, IntVec3 loc)
		{
			TerrainInstance terrainInstance = (TerrainInstance)Activator.CreateInstance(tDef.terrainInstanceClass);
			terrainInstance.def = tDef;
			terrainInstance.Map = map;
			terrainInstance.Position = loc;
			return terrainInstance;
		}

		// Token: 0x04000008 RID: 8
		public static readonly Material NeedsPowerMat = MaterialPool.MatFrom("UI/Overlays/NeedsPower", ShaderDatabase.MetaOverlay);
	}
}
