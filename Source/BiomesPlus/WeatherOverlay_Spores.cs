using System;
using UnityEngine;
using Verse;

namespace BiomesPlus
{
	// Token: 0x02000008 RID: 8
	[StaticConstructorOnStartup]
	public class WeatherOverlay_Spores : SkyOverlay
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000246C File Offset: 0x0000066C
		public WeatherOverlay_Spores()
		{
			this.worldOverlayMat = WeatherOverlay_Spores.SnowGentleOverlayWorld;
			this.worldOverlayPanSpeed1 = 0.0005f;
			this.worldOverlayPanSpeed2 = 0.0004f;
			this.worldPanDir1 = new Vector2(1f, 1f);
			this.worldPanDir2 = new Vector2(1f, -1f);
		}

		// Token: 0x04000002 RID: 2
		private static readonly Material SnowGentleOverlayWorld = MatLoader.LoadMat("Weather/SnowOverlayWorld", -1);
	}
}
