using System;
using RimWorld;
using Verse;

namespace ActiveTerrain;

public sealed class TerrainComp_Glower : TerrainComp
{
    [Unsaved] private bool currentlyOn;

    [Unsaved] private CompGlower instanceGlowerComp;

    private CompGlower AsThingComp => instanceGlowerComp ?? (instanceGlowerComp = (CompGlower)this);

    private TerrainCompProperties_Glower Props => (TerrainCompProperties_Glower)props;

    private bool ShouldBeLitNow
    {
        get
        {
            var comp = parent.GetComp<TerrainComp_PowerTrader>();
            return comp == null || comp.PowerOn || !Props.powered;
        }
    }

    private float OverlightRadius { get; set; }

    private float GlowRadius { get; set; }

    private ColorInt Color { get; set; }

    private void updateLit()
    {
        var shouldBeLitNow = ShouldBeLitNow;
        if (currentlyOn == shouldBeLitNow)
        {
            return;
        }

        currentlyOn = shouldBeLitNow;
        parent.Map.mapDrawer.MapMeshDirty(parent.Position, MapMeshFlagDefOf.Things);
        (currentlyOn
            ? parent.Map.glowGrid.RegisterGlower
            : new Action<CompGlower>(parent.Map.glowGrid.DeRegisterGlower))(AsThingComp);
    }

    public override void ReceiveCompSignal(string sig)
    {
        base.ReceiveCompSignal(sig);
        if (sig == CompSignals.PowerTurnedOff || sig == CompSignals.PowerTurnedOn)
        {
            updateLit();
        }
    }

    public override void PostPostLoad()
    {
        updateLit();
        var shouldBeLitNow = ShouldBeLitNow;
        if (shouldBeLitNow)
        {
            parent.Map.glowGrid.RegisterGlower(AsThingComp);
        }
    }

    public override void Initialize(TerrainCompProperties props)
    {
        base.Initialize(props);
        Color = Props.glowColor;
        GlowRadius = Props.glowRadius;
        OverlightRadius = Props.overlightRadius;
    }

    public static explicit operator CompGlower(TerrainComp_Glower inst)
    {
        var compGlower = new CompGlower
        {
            parent = (ThingWithComps)ThingMaker.MakeThing(ThingDefOf.Wall, ThingDefOf.Steel)
        };
        compGlower.parent.SetPositionDirect(inst.parent.Position);
        compGlower.Initialize(new CompProperties_Glower
        {
            glowColor = inst.Color,
            glowRadius = inst.GlowRadius,
            overlightRadius = inst.OverlightRadius
        });
        return compGlower;
    }
}