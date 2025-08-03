using RimWorld;
using Verse;

namespace ActiveTerrain;

public class TerrainComp_SelfClean : TerrainComp
{
    private float cleanProgress = float.NaN;

    private Filth currentFilth;

    public TerrainCompProperties_SelfClean Props => (TerrainCompProperties_SelfClean)props;

    protected virtual bool CanClean => true;

    private void StartClean()
    {
        if (currentFilth == null)
        {
            Log.Warning("Cannot start clean for filth because there is no filth selected. Canceling.");
        }
        else
        {
            if (currentFilth.def.filth == null)
            {
                Log.Error(
                    $"Filth of def {currentFilth.def.defName} cannot be cleaned because it has no FilthProperties.");
            }
            else
            {
                cleanProgress = currentFilth.def.filth.cleaningWorkToReduceThickness;
            }
        }
    }

    public override void CompTick()
    {
        base.CompTick();
        var canClean = CanClean;
        if (canClean)
        {
            DoCleanWork();
        }
    }

    protected virtual void DoCleanWork()
    {
        if (currentFilth == null)
        {
            cleanProgress = float.NaN;
            if (!findFilth())
            {
                return;
            }
        }

        if (float.IsNaN(cleanProgress))
        {
            StartClean();
        }

        if (cleanProgress > 0f)
        {
            cleanProgress -= 1f;
        }
        else
        {
            finishClean();
        }
    }

    private bool findFilth()
    {
        bool result;
        if (currentFilth != null)
        {
            result = true;
        }
        else
        {
            var filth = (Filth)parent.Position.GetThingList(parent.Map).Find(f => f is Filth);
            if (filth != null)
            {
                currentFilth = filth;
                result = true;
            }
            else
            {
                result = false;
            }
        }

        return result;
    }

    private void finishClean()
    {
        if (currentFilth == null)
        {
            Log.Warning("Cannot finish clean for filth because there is no filth selected. Canceling.");
        }
        else
        {
            currentFilth.ThinFilth();
            var destroyed = currentFilth.Destroyed;
            if (destroyed)
            {
                currentFilth = null;
            }
            else
            {
                cleanProgress = float.NaN;
            }
        }
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref cleanProgress, "cleanProgress", float.NaN);
        Scribe_References.Look(ref currentFilth, "currentFilth");
    }
}