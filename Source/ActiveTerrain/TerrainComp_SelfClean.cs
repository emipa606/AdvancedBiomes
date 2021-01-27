using System;
using RimWorld;
using Verse;

namespace ActiveTerrain
{
	// Token: 0x02000013 RID: 19
	public class TerrainComp_SelfClean : TerrainComp
	{
        // Token: 0x17000007 RID: 7
        // (get) Token: 0x06000043 RID: 67 RVA: 0x00002DA4 File Offset: 0x00000FA4
        public TerrainCompProperties_SelfClean Props => (TerrainCompProperties_SelfClean)props;

        // Token: 0x17000008 RID: 8
        // (get) Token: 0x06000044 RID: 68 RVA: 0x00002DC4 File Offset: 0x00000FC4
        protected virtual bool CanClean => true;

        // Token: 0x06000045 RID: 69 RVA: 0x00002DD8 File Offset: 0x00000FD8
        public void StartClean()
		{
			var flag = currentFilth == null;
			if (flag)
			{
				Log.Warning("Cannot start clean for filth because there is no filth selected. Canceling.", false);
			}
			else
			{
				var flag2 = currentFilth.def.filth == null;
				if (flag2)
				{
					Log.Error(string.Format("Filth of def {0} cannot be cleaned because it has no FilthProperties.", currentFilth.def.defName), false);
				}
				else
				{
					cleanProgress = currentFilth.def.filth.cleaningWorkToReduceThickness;
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E58 File Offset: 0x00001058
		public override void CompTick()
		{
			base.CompTick();
			var canClean = CanClean;
			if (canClean)
			{
				DoCleanWork();
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E80 File Offset: 0x00001080
		public virtual void DoCleanWork()
		{
			var flag = currentFilth == null;
			if (flag)
			{
				cleanProgress = float.NaN;
				var flag2 = !FindFilth();
				if (flag2)
				{
					return;
				}
			}
			var flag3 = float.IsNaN(cleanProgress);
			if (flag3)
			{
				StartClean();
			}
			var flag4 = cleanProgress > 0f;
			if (flag4)
			{
				cleanProgress -= 1f;
			}
			else
			{
				FinishClean();
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool FindFilth()
		{
			var flag = currentFilth != null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				var filth = (Filth)parent.Position.GetThingList(parent.Map).Find((Thing f) => f is Filth);
				var flag2 = filth != null;
				if (flag2)
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

		// Token: 0x06000049 RID: 73 RVA: 0x00002F7C File Offset: 0x0000117C
		public void FinishClean()
		{
			var flag = currentFilth == null;
			if (flag)
			{
				Log.Warning("Cannot finish clean for filth because there is no filth selected. Canceling.", false);
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

		// Token: 0x0600004A RID: 74 RVA: 0x00002FD5 File Offset: 0x000011D5
		public override void PostExposeData()
		{
			base.PostExposeData();
			Scribe_Values.Look<float>(ref cleanProgress, "cleanProgress", float.NaN, false);
			Scribe_References.Look<Filth>(ref currentFilth, "currentFilth", false);
		}

		// Token: 0x04000020 RID: 32
		public float cleanProgress = float.NaN;

		// Token: 0x04000021 RID: 33
		public Filth currentFilth;
	}
}
