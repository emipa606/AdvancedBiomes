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
    public static BiomesPlusMod instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private BiomesPlusSettings settings;

    /// <summary>
    ///     Cunstructor
    /// </summary>
    /// <param name="content"></param>
    public BiomesPlusMod(ModContentPack content) : base(content)
    {
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(ModLister.GetActiveModWithIdentifier("Mlie.AdvancedBiomes"));
        instance = this;
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal BiomesPlusSettings Settings
    {
        get
        {
            if (settings == null)
            {
                settings = GetSettings<BiomesPlusSettings>();
            }

            return settings;
        }
        set => settings = value;
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
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        listing_Standard.Label("AdB.Probability.label".Translate());
        listing_Standard.Gap();
        listing_Standard.Label("AdB.PoisonForest.label".Translate(Settings.PoisonForest));
        Settings.PoisonForest = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.PoisonForest, 0,
            200f, false, null, null, null, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("AdB.Savanna.label".Translate(Settings.Savanna));
        Settings.Savanna = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Savanna, 0, 200f, false,
            null, null, null, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("AdB.Volcano.label".Translate(Settings.Volcano));
        Settings.Volcano = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Volcano, 0, 200f, false,
            null, null, null, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("AdB.Wasteland.label".Translate(Settings.Wasteland));
        Settings.Wasteland = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Wasteland, 0, 200f,
            false, null, null, null, 1f);
        listing_Standard.Gap();
        listing_Standard.Label("AdB.Wetland.label".Translate(Settings.Wetland));
        Settings.Wetland = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Wetland, 0, 200f, false,
            null, null, null, 1f);
        listing_Standard.GapLine();
        listing_Standard.CheckboxLabeled("Enable volcano variety", ref Settings.VolcanoVariety,
            "AdB.VolcanoVariety.tooltip".Translate());
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("AdB.CurrentModVersion.label".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        Settings.Write();
    }
}