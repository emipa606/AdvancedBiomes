using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace BiomesPlus;

public class GameCondition_Spores : GameCondition
{
    private const int LerpTicks = 5000;

    private const float MaxSkyLerpFactor = 0.5f;

    private const float SkyGlow = 0.85f;

    private const int CheckInterval = 3451;

    private const float ToxicPerDay = 0.15f;

    private readonly List<SkyOverlay> overlays;

    private SkyColorSet ToxicFalloutColors;

    public GameCondition_Spores()
    {
        var colorInt = new ColorInt(216, 255, 0);
        var toColor = colorInt.ToColor;
        var colorInt2 = new ColorInt(234, 200, 255);
        ToxicFalloutColors = new SkyColorSet(toColor, colorInt2.ToColor, new Color(1f, 1f, 1f), SkyGlow);
        overlays = [new WeatherOverlay_Spores()];
    }

    public override void Init()
    {
        LessonAutoActivator.TeachOpportunity(ConceptDefOf.ForbiddingDoors, OpportunityType.Critical);
        LessonAutoActivator.TeachOpportunity(ConceptDefOf.AllowedAreas, OpportunityType.Critical);
    }

    public override void GameConditionTick()
    {
        var affectedMaps = AffectedMaps;
        if (Find.TickManager.TicksGame % CheckInterval == 0)
        {
            foreach (var map in affectedMaps)
            {
                doPawnsPoisonDamage(map);
            }
        }

        foreach (var skyOverlay in overlays)
        {
            foreach (var map in affectedMaps)
            {
                skyOverlay.TickOverlay(map, 1f);
            }
        }
    }

    private static void doPawnsPoisonDamage(Map map)
    {
        var allPawnsSpawned = map.mapPawns.AllPawnsSpawned;
        foreach (var pawn in allPawnsSpawned)
        {
            if (pawn.Position.Roofed(map) || !pawn.def.race.IsFlesh)
            {
                continue;
            }

            var num = 0.028758334f;
            num *= 1 - pawn.GetStatValue(StatDefOf.ToxicResistance);
            if (num == 0f)
            {
                continue;
            }

            var num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(pawn.thingIDNumber ^ 74374237));
            num *= num2;
            HealthUtility.AdjustSeverity(pawn, HediffDefOf.PoisonBuildup, num);
        }
    }
}

[DefOf]
public static class BiomeDefOf
{
    public static BiomeDef PoisonForest;

    public static BiomeDef Savanna;

    public static BiomeDef Wetland;

    public static BiomeDef Volcano;
}

[DefOf]
public static class HediffDefOf
{
    public static HediffDef PoisonBuildup;
}

public static class Util_PoisonForestBiome
{
    public static BiomeDef PoisonForestBiomeDef => BiomeDef.Named("PoisonForest");

    public static BiomeDef SavannaBiomeDef => BiomeDef.Named("Savanna");

    public static BiomeDef WetlandBiomeDef => BiomeDef.Named("Wetland");

    public static BiomeDef VolcanoBiomeDef => BiomeDef.Named("Volcano");

    public static GameConditionDef PoisonForestEnvironmentGameConditionDef => GameConditionDef.Named("Spores");
}