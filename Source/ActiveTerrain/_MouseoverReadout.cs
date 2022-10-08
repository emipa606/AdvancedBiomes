using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Verse;

namespace ActiveTerrain
{
    [HarmonyPatch(typeof(MouseoverReadout), "MouseoverReadoutOnGUI", null)]
    internal static class _MouseoverReadout
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> original)
        {
            foreach (var instr in original)
            {
                if (false && instr.opcode == OpCodes.Ldfld && (FieldInfo) instr.operand ==
                    typeof(MouseoverReadout).GetField("cachedTerrain", BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    yield return instr;
                    yield return new CodeInstruction(OpCodes.Ceq);
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 10);
                    yield return new CodeInstruction(OpCodes.Isinst, typeof(SpecialTerrain));
                    yield return new CodeInstruction(OpCodes.Ldnull);
                    yield return new CodeInstruction(OpCodes.Ceq);
                    yield return new CodeInstruction(OpCodes.And);
                    yield return new CodeInstruction(OpCodes.Ldc_I4_1);
                }

                var patchedCodeBlock2 = false;
                if (false && !patchedCodeBlock2 && instr.opcode == OpCodes.Callvirt &&
                    (MethodInfo) instr.operand == typeof(Def).GetMethod("get_LabelCap"))
                {
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Find).GetMethod("get_CurrentMap"));
                    yield return new CodeInstruction(OpCodes.Call,
                        typeof(_MouseoverReadout).GetMethod("HandleLabelQuery"));
                    patchedCodeBlock2 = true;
                }

                yield return instr;
            }
        }

        public static string HandleLabelQuery(TerrainDef def, IntVec3 loc, Map map)
        {
            string result;
            if (def is SpecialTerrain)
            {
                var terrainInstance = map.GetComponent<SpecialTerrainList>().terrains[loc];
                if (terrainInstance.def != def)
                {
                    Log.Warning(
                        $"ActiveTerrain :: Got terrain instance at tile {loc} but def of terrain instance ({terrainInstance.def.defName}) isn't equal to the def on the mouseover readout ({def.defName}). Using the former.");
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
