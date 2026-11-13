using Mlie;
using UnityEngine;
using Verse;

namespace BiomesPlus;

[StaticConstructorOnStartup]
internal class BiomesPlusMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static BiomesPlusMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     Cunstructor
    /// </summary>
    /// <param name="content"></param>
    public BiomesPlusMod(ModContentPack content) : base(content)
    {
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        Instance = this;
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    private BiomesPlusSettings Settings
    {
        get
        {
            field ??= GetSettings<BiomesPlusSettings>();

            return field;
        }
    }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Advanced Biomes";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="inRect"></param>
    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.Label("AdB.Probability.label".Translate());
        listingStandard.Gap();
        listingStandard.Label("AdB.PoisonForest.label".Translate(Settings.PoisonForest));
        Settings.PoisonForest = Widgets.HorizontalSlider(listingStandard.GetRect(20), Settings.PoisonForest, 0,
            200f, false, null, null, null, 1f);
        listingStandard.Gap();
        listingStandard.Label("AdB.Savanna.label".Translate(Settings.Savanna));
        Settings.Savanna = Widgets.HorizontalSlider(listingStandard.GetRect(20), Settings.Savanna, 0, 200f,
            false,
            null, null, null, 1f);
        listingStandard.Gap();
        listingStandard.Label("AdB.Volcano.label".Translate(Settings.Volcano));
        Settings.Volcano = Widgets.HorizontalSlider(listingStandard.GetRect(20), Settings.Volcano, 0, 200f,
            false,
            null, null, null, 1f);
        listingStandard.Gap();
        listingStandard.Label("AdB.Wasteland.label".Translate(Settings.Wasteland));
        Settings.Wasteland = Widgets.HorizontalSlider(listingStandard.GetRect(20), Settings.Wasteland, 0, 200f,
            false, null, null, null, 1f);
        listingStandard.Gap();
        listingStandard.Label("AdB.Wetland.label".Translate(Settings.Wetland));
        Settings.Wetland = Widgets.HorizontalSlider(listingStandard.GetRect(20), Settings.Wetland, 0, 200f,
            false,
            null, null, null, 1f);
        listingStandard.GapLine();
        listingStandard.CheckboxLabeled("Enable volcano variety", ref Settings.VolcanoVariety,
            "AdB.VolcanoVariety.tooltip".Translate());
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("AdB.CurrentModVersion.label".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        Settings.Write();
    }
}