using Verse;

namespace ActiveTerrain;

public class TerrainComp_PowerTrader : TerrainComp
{
    public readonly int tickInterval = 50;

    private CompPowerTraderFloor connectParentInt;

    private bool curSignal;

    private float powerOutputInt;

    public CompPowerTraderFloor ConnectParent
    {
        get => connectParentInt;
        set
        {
            var compPowerTraderFloor = connectParentInt;
            compPowerTraderFloor?.Notify_TerrainCompRemoved(this);

            connectParentInt = value;
            value?.ReceiveTerrainComp(this);
        }
    }

    public TerrainCompProperties_PowerTrader Props => (TerrainCompProperties_PowerTrader)props;

    public bool PowerOn => ConnectParent is { PowerOn: true };

    public virtual float PowerOutput
    {
        get => powerOutputInt;
        set
        {
            powerOutputInt = value;
            var connectParent = ConnectParent;
            connectParent?.UpdatePowerOutput();
        }
    }

    public override void CompUpdate()
    {
        if (!PowerOn && !Props.ignoreNeedsPower)
        {
            ActiveTerrainUtility.RenderPulsingNeedsPowerOverlay(parent.Position);
        }
    }

    public override void CompTick()
    {
        if (PowerOn != curSignal)
        {
            parent.BroadcastCompSignal(PowerOn ? CompSignals.PowerTurnedOn : CompSignals.PowerTurnedOff);
            curSignal = PowerOn;
        }

        if (PowerOn || Find.TickManager.TicksGame % tickInterval != this.HashCodeToMod(tickInterval))
        {
            return;
        }

        var compPowerTraderFloor =
            ActiveTerrainUtility.TryFindNearestPowerConduitFloor(parent.Position, parent.Map);
        if (compPowerTraderFloor != null)
        {
            ConnectParent = compPowerTraderFloor;
        }
    }

    public override void Initialize(TerrainCompProperties props)
    {
        base.Initialize(props);
        powerOutputInt = -Props.basePowerConsumption;
    }

    public override void PostRemove()
    {
        ConnectParent = null;
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref curSignal, "curCompSignal");
        Thing thing = null;
        if (Scribe.mode == LoadSaveMode.Saving && ConnectParent != null)
        {
            thing = ConnectParent.parent;
        }

        Scribe_References.Look(ref thing, "parentThing");
        if (thing != null)
        {
            ConnectParent = ((ThingWithComps)thing).GetComp<CompPowerTraderFloor>();
        }
    }
}