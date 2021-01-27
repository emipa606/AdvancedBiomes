using Verse;

namespace BiomesPlus
{
    /// <summary>
    /// Definition of the settings for the mod
    /// </summary>
    internal class BiomesPlusSettings : ModSettings
    {
        public float PoisonForest = 100f;
        public float Savanna = 100f;
        public float Volcano = 100f;
        public float Wasteland = 100f;
        public float Wetland = 100f;
        public bool VolcanoVariety = false;

        /// <summary>
        /// Saving and loading the values
        /// </summary>
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref PoisonForest, "PoisonForest", 100f, false);
            Scribe_Values.Look(ref Savanna, "Savanna", 100f, false);
            Scribe_Values.Look(ref Volcano, "Volcano", 100f, false);
            Scribe_Values.Look(ref Wasteland, "Wasteland", 100f, false);
            Scribe_Values.Look(ref Wetland, "Wetland", 100f, false);
            Scribe_Values.Look(ref VolcanoVariety, "VolcanoVariety", false, false);
        }
    }
}