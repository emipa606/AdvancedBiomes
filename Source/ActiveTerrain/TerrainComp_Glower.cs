using System;
using RimWorld;
using Verse;

namespace ActiveTerrain;

public class TerrainComp_Glower : TerrainComp
{
    [Unsaved] protected bool currentlyOn;

    [Unsaved] private CompGlower instanceGlowerComp;

    public CompGlower AsThingComp => instanceGlowerComp ?? (instanceGlowerComp = (CompGlower)this);

    public TerrainCompProperties_Glower Props => (TerrainCompProperties_Glower)props;

    public virtual bool ShouldBeLitNow
    {
        get
        {
            var comp = parent.GetComp<TerrainComp_PowerTrader>();
            return comp == null || comp.PowerOn || !Props.powered;
        }
    }

    public float OverlightRadius { get; set; }

    public float GlowRadius { get; set; }

    public ColorInt Color { get; set; }

    public void UpdateLit()
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
            UpdateLit();
        }
    }

    public override void PostPostLoad()
    {
        UpdateLit();
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