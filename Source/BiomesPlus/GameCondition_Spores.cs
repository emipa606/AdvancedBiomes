using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000009 RID: 9
    public class GameCondition_Spores : GameCondition
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000024E0 File Offset: 0x000006E0
		public GameCondition_Spores()
		{
			var colorInt = new ColorInt(216, 255, 0);
			Color toColor = colorInt.ToColor;
			var colorInt2 = new ColorInt(234, 200, 255);
			ToxicFalloutColors = new SkyColorSet(toColor, colorInt2.ToColor, new Color(1f, 1f, 1f), SkyGlow);
			overlays = new List<SkyOverlay>
			{
				new WeatherOverlay_Spores()
			};
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000256D File Offset: 0x0000076D
		public override void Init()
		{
			LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
			LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002588 File Offset: 0x00000788
		public override void GameConditionTick()
		{
			List<Map> affectedMaps = base.AffectedMaps;
			if (Find.TickManager.TicksGame % CheckInterval == 0)
			{
				for (var i = 0; i < affectedMaps.Count; i++)
				{
					DoPawnsPoisonDamage(affectedMaps[i]);
				}
			}
			for (var j = 0; j < overlays.Count; j++)
			{
				for (var k = 0; k < affectedMaps.Count; k++)
				{
					overlays[j].TickOverlay(affectedMaps[k]);
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002634 File Offset: 0x00000834
		private void DoPawnsPoisonDamage(Map map)
		{
			List<Pawn> allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
			for (var i = 0; i < allPawnsSpawned.Count; i++)
			{
				Pawn pawn = allPawnsSpawned[i];
				if (!pawn.Position.Roofed(map) && pawn.def.race.IsFlesh)
				{
					var num = 0.028758334f;
					num *= pawn.GetStatValue(StatDefOf.ToxicSensitivity, true);
					if (num != 0f)
					{
						var num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(pawn.thingIDNumber ^ 74374237));
						num *= num2;
						HealthUtility.AdjustSeverity(pawn, GameCondition_Spores.HediffDefOf.PoisonBuildup, num);
					}
				}
			}
		}

		// Token: 0x04000003 RID: 3
		private const int LerpTicks = 5000;

		// Token: 0x04000004 RID: 4
		private const float MaxSkyLerpFactor = 0.5f;

		// Token: 0x04000005 RID: 5
		private const float SkyGlow = 0.85f;

		// Token: 0x04000006 RID: 6
		private const int CheckInterval = 3451;

		// Token: 0x04000007 RID: 7
		private const float ToxicPerDay = 0.15f;

		// Token: 0x04000008 RID: 8
		private SkyColorSet ToxicFalloutColors;

		// Token: 0x04000009 RID: 9
		private readonly List<SkyOverlay> overlays;

		// Token: 0x0200000A RID: 10
		[DefOf]
		public static class BiomeDefOf
		{
			// Token: 0x0400000A RID: 10
			public static BiomeDef PoisonForest;

			// Token: 0x0400000B RID: 11
			public static BiomeDef Savanna;

			// Token: 0x0400000C RID: 12
			public static BiomeDef Wetland;

			// Token: 0x0400000D RID: 13
			public static BiomeDef Volcano;
		}

		// Token: 0x0200000B RID: 11
		[DefOf]
		public static class HediffDefOf
		{
			// Token: 0x0400000E RID: 14
			public static HediffDef PoisonBuildup;
		}

		// Token: 0x0200000C RID: 12
		public static class Util_PoisonForestBiome
		{
            // Token: 0x17000001 RID: 1
            // (get) Token: 0x06000014 RID: 20 RVA: 0x000026FC File Offset: 0x000008FC
            public static BiomeDef PoisonForestBiomeDef => BiomeDef.Named("PoisonForest");

            // Token: 0x17000002 RID: 2
            // (get) Token: 0x06000015 RID: 21 RVA: 0x00002718 File Offset: 0x00000918
            public static BiomeDef SavannaBiomeDef => BiomeDef.Named("Savanna");

            // Token: 0x17000003 RID: 3
            // (get) Token: 0x06000016 RID: 22 RVA: 0x00002734 File Offset: 0x00000934
            public static BiomeDef WetlandBiomeDef => BiomeDef.Named("Wetland");

            // Token: 0x17000004 RID: 4
            // (get) Token: 0x06000017 RID: 23 RVA: 0x00002750 File Offset: 0x00000950
            public static BiomeDef VolcanoBiomeDef => BiomeDef.Named("Volcano");

            // Token: 0x17000005 RID: 5
            // (get) Token: 0x06000018 RID: 24 RVA: 0x0000276C File Offset: 0x0000096C
            public static GameConditionDef PoisonForestEnvironmentGameConditionDef => GameConditionDef.Named("Spores");
        }
	}
}
