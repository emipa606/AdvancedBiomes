using System;
using System.Collections.Generic;
using Verse;

namespace ActiveTerrain
{
    // Token: 0x02000009 RID: 9
    public class TerrainInstance : IExposable
    {
        // Token: 0x0400000C RID: 12
        public List<TerrainComp> comps = new List<TerrainComp>();

        // Token: 0x0400000B RID: 11
        public SpecialTerrain def;

        // Token: 0x0400000D RID: 13
        private Map mapInt;

        // Token: 0x0400000E RID: 14
        private IntVec3 positionInt;

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000014 RID: 20 RVA: 0x00002598 File Offset: 0x00000798
        // (set) Token: 0x06000015 RID: 21 RVA: 0x000025B0 File Offset: 0x000007B0
        public Map Map
        {
            get => mapInt;
            set => mapInt = value;
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000016 RID: 22 RVA: 0x000025BC File Offset: 0x000007BC
        // (set) Token: 0x06000017 RID: 23 RVA: 0x000025D4 File Offset: 0x000007D4
        public IntVec3 Position
        {
            get => positionInt;
            set => positionInt = value;
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600001A RID: 26 RVA: 0x00002600 File Offset: 0x00000800
        public virtual string Label
        {
            get
            {
                string text = def.LabelCap;
                foreach (var terrainComp in comps)
                {
                    text = terrainComp.TransformLabel(text);
                }

                return text;
            }
        }

        // Token: 0x06000023 RID: 35 RVA: 0x000028A4 File Offset: 0x00000AA4
        public virtual void ExposeData()
        {
            Scribe_References.Look(ref mapInt, "map");
            Scribe_Values.Look(ref positionInt, "pos");
            Scribe_Defs.Look(ref def, "def");
            if (Scribe.mode == LoadSaveMode.LoadingVars)
            {
                InitializeComps();
            }

            foreach (var terrainComp in comps)
            {
                terrainComp.PostExposeData();
            }
        }

        // Token: 0x06000019 RID: 25 RVA: 0x000025F3 File Offset: 0x000007F3
        public virtual void Init()
        {
            InitializeComps();
        }

        // Token: 0x0600001B RID: 27 RVA: 0x00002650 File Offset: 0x00000850
        public T GetComp<T>() where T : TerrainComp
        {
            foreach (var terrainComp in comps)
            {
                T result;
                if ((result = terrainComp as T) != null)
                {
                    return result;
                }
            }

            return default;
        }

        // Token: 0x0600001C RID: 28 RVA: 0x000026B4 File Offset: 0x000008B4
        public void InitializeComps()
        {
            foreach (var terrainCompProperties in def.terrainComps)
            {
                var terrainComp = (TerrainComp) Activator.CreateInstance(terrainCompProperties.compClass);
                terrainComp.parent = this;
                comps.Add(terrainComp);
                terrainComp.Initialize(terrainCompProperties);
            }
        }

        // Token: 0x0600001D RID: 29 RVA: 0x00002738 File Offset: 0x00000938
        public virtual void Tick()
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.CompTick();
            }
        }

        // Token: 0x0600001E RID: 30 RVA: 0x00002774 File Offset: 0x00000974
        public virtual void Update()
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.CompUpdate();
            }
        }

        // Token: 0x0600001F RID: 31 RVA: 0x000027B0 File Offset: 0x000009B0
        public virtual void PostPlacedDown()
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.PlaceSetup();
            }
        }

        // Token: 0x06000020 RID: 32 RVA: 0x000027EC File Offset: 0x000009EC
        public virtual void PostRemove()
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.PostRemove();
            }
        }

        // Token: 0x06000021 RID: 33 RVA: 0x00002828 File Offset: 0x00000A28
        public virtual void PostLoad()
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.PostPostLoad();
            }
        }

        // Token: 0x06000022 RID: 34 RVA: 0x00002864 File Offset: 0x00000A64
        public virtual void BroadcastCompSignal(string sig)
        {
            foreach (var terrainComp in comps)
            {
                terrainComp.ReceiveCompSignal(sig);
            }
        }
    }
}