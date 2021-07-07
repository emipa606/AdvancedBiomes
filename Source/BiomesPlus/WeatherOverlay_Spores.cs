using UnityEngine;
using Verse;

namespace BiomesPlus
{
    // Token: 0x02000008 RID: 8
    [StaticConstructorOnStartup]
    public class WeatherOverlay_Spores : SkyOverlay
    {
        // Token: 0x04000002 RID: 2
        private static readonly Material SnowGentleOverlayWorld = MatLoader.LoadMat("Weather/SnowOverlayWorld");

        // Token: 0x0600000E RID: 14 RVA: 0x0000246C File Offset: 0x0000066C
        public WeatherOverlay_Spores()
        {
            worldOverlayMat = SnowGentleOverlayWorld;
            worldOverlayPanSpeed1 = 0.0005f;
            worldOverlayPanSpeed2 = 0.0004f;
            worldPanDir1 = new Vector2(1f, 1f);
            worldPanDir2 = new Vector2(1f, -1f);
        }
    }
}