using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000007 RID: 7
	[HarmonyPatch(typeof(MouseoverReadout), "MouseoverReadoutOnGUI", null)]
	internal static class _MouseoverReadout
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002484 File Offset: 0x00000684
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> original)
		{
            bool patchedCodeBlock2 = false;
			foreach (CodeInstruction instr in original)
			{
                yield return instr;
                continue;
                bool flag = instr.opcode == OpCodes.Ldfld && instr.operand == typeof(MouseoverReadout).GetField("cachedTerrain", BindingFlags.Instance | BindingFlags.NonPublic);
				if (flag)
				{
					yield return instr;
					yield return new CodeInstruction(OpCodes.Ceq, null);
					yield return new CodeInstruction(OpCodes.Ldloc_S, 10);
					yield return new CodeInstruction(OpCodes.Isinst, typeof(SpecialTerrain));
					yield return new CodeInstruction(OpCodes.Ldnull, null);
					yield return new CodeInstruction(OpCodes.Ceq, null);
					yield return new CodeInstruction(OpCodes.And, null);
					yield return new CodeInstruction(OpCodes.Ldc_I4_1, null);
				}
				else
				{
					bool flag2 = !patchedCodeBlock2 && instr.opcode == OpCodes.Callvirt && instr.operand == typeof(Def).GetMethod("get_LabelCap");
					if (flag2)
					{
						yield return new CodeInstruction(OpCodes.Ldloc_0, null);
						yield return new CodeInstruction(OpCodes.Call, typeof(Find).GetMethod("get_CurrentMap"));
						yield return new CodeInstruction(OpCodes.Call, typeof(_MouseoverReadout).GetMethod("HandleLabelQuery"));
						patchedCodeBlock2 = true;
					}
					else
					{
						yield return instr;
					}
				}
			}
			IEnumerator<CodeInstruction> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002494 File Offset: 0x00000694
		public static string HandleLabelQuery(TerrainDef def, IntVec3 loc, Map map)
		{
			bool flag = def is SpecialTerrain;
			string result;
			if (flag)
			{
				TerrainInstance terrainInstance = map.GetComponent<SpecialTerrainList>().terrains[loc];
				bool flag2 = terrainInstance.def != def;
				if (flag2)
				{
					Log.Warning(string.Format("ActiveTerrain :: Got terrain instance at tile {0} but def of terrain instance ({1}) isn't equal to the def on the mouseover readout ({2}). Using the former.", loc, terrainInstance.def.defName, def.defName), false);
				}
				result = terrainInstance.Label;
			}
			else
			{
				result = def.LabelCap;
			}
			return result;
		}
	}
}
