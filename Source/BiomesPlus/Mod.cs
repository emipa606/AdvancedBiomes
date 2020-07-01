using UnityEngine;
using Verse;

namespace BiomesPlus
{
    [StaticConstructorOnStartup]
    internal class BiomesPlusMod : Mod
    {
        /// <summary>
        /// Cunstructor
        /// </summary>
        /// <param name="content"></param>
        public BiomesPlusMod(ModContentPack content) : base(content)
        {
            instance = this;
        }

        /// <summary>
        /// The instance-settings for the mod
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
            set
            {
                settings = value;
            }
        }

        /// <summary>
        /// The title for the mod-settings
        /// </summary>
        /// <returns></returns>
        public override string SettingsCategory()
        {
            return "Advanced Biomes";
        }

        /// <summary>
        /// The settings-window
        /// For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
        /// </summary>
        /// <param name="rect"></param>
        public override void DoSettingsWindowContents(Rect rect)
        {
            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(rect);
            listing_Standard.Label("Increase or lower the probability during world-generation, 100% is the standard value");
            listing_Standard.Gap();
            listing_Standard.Label("PoisonForest: " + Settings.PoisonForest + "%");
            Settings.PoisonForest = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.PoisonForest, 0, 200f, false, null, null, null, 1f);
            listing_Standard.Gap();
            listing_Standard.Label("Savanna: " + Settings.Savanna + "%");
            Settings.Savanna = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Savanna, 0, 200f, false, null, null, null, 1f);
            listing_Standard.Gap();
            listing_Standard.Label("Volcano: " + Settings.Volcano + "%");
            Settings.Volcano = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Volcano, 0, 200f, false, null, null, null, 1f);
            listing_Standard.Gap();
            listing_Standard.Label("Wasteland: " + Settings.Wasteland+ "%");
            Settings.Wasteland = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Wasteland, 0, 200f, false, null, null, null, 1f);
            listing_Standard.Gap();
            listing_Standard.Label("Wetland: " + Settings.Wetland + "%");
            Settings.Wetland = Widgets.HorizontalSlider(listing_Standard.GetRect(20), Settings.Wetland, 0, 200f, false, null, null, null, 1f);
            listing_Standard.End();
            Settings.Write();
        }

        /// <summary>
        /// The instance of the settings to be read by the mod
        /// </summary>
        public static BiomesPlusMod instance;

        /// <summary>
        /// The private settings
        /// </summary>
        private BiomesPlusSettings settings;

    }
}
