namespace Quester
{
    internal struct Mob
    {
        public ushort Null1;
        public MobType Type;
        public ushort Count;
        public string Variable;
        public uint Null2;
        public short Index;

        public override string ToString()
        {
            return $"{Variable}: {Count} {Type}";
        }
    }
}