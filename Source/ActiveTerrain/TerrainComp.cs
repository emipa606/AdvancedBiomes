namespace ActiveTerrain
{
    public class TerrainComp
    {
        public TerrainInstance parent;

        public TerrainCompProperties props;

        public virtual void Initialize(TerrainCompProperties props)
        {
            this.props = props;
        }

        public virtual string TransformLabel(string label)
        {
            return label;
        }

        public virtual void CompTick()
        {
        }

        public virtual void CompUpdate()
        {
        }

        public virtual void PlaceSetup()
        {
        }

        public virtual void PostRemove()
        {
        }

        public virtual void PostExposeData()
        {
        }

        public virtual void ReceiveCompSignal(string sig)
        {
        }

        public virtual void PostPostLoad()
        {
        }
    }
}
