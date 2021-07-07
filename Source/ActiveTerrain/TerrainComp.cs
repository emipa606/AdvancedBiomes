namespace ActiveTerrain
{
    // Token: 0x0200000C RID: 12
    public class TerrainComp
    {
        // Token: 0x04000011 RID: 17
        public TerrainInstance parent;

        // Token: 0x04000012 RID: 18
        public TerrainCompProperties props;

        // Token: 0x0600002F RID: 47 RVA: 0x00002BD0 File Offset: 0x00000DD0
        public virtual void Initialize(TerrainCompProperties props)
        {
            this.props = props;
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00002BDC File Offset: 0x00000DDC
        public virtual string TransformLabel(string label)
        {
            return label;
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void CompTick()
        {
        }

        // Token: 0x06000032 RID: 50 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void CompUpdate()
        {
        }

        // Token: 0x06000033 RID: 51 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void PlaceSetup()
        {
        }

        // Token: 0x06000034 RID: 52 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void PostRemove()
        {
        }

        // Token: 0x06000035 RID: 53 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void PostExposeData()
        {
        }

        // Token: 0x06000036 RID: 54 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void ReceiveCompSignal(string sig)
        {
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00002BEF File Offset: 0x00000DEF
        public virtual void PostPostLoad()
        {
        }
    }
}