using UnityEngine;
using Verse;

namespace BiomesPlus;

[StaticConstructorOnStartup]
public class WeatherOverlay_Spores : WeatherOverlayDualPanner
{
    private static readonly Material snowGentleOverlayWorld = MatLoader.LoadMat("Weather/SnowOverlayWorld");

    public WeatherOverlay_Spores()
    {
        worldOverlayMat = snowGentleOverlayWorld;
        worldOverlayPanSpeed1 = 0.0005f;
        worldOverlayPanSpeed2 = 0.0004f;
        worldPanDir1 = new Vector2(1f, 1f);
        worldPanDir2 = new Vector2(1f, -1f);
    }
}