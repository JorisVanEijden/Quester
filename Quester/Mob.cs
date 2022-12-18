namespace Quester
{
    internal struct Mob
    {
        public ushort Null1;
        public MobType Type;
        public ushort Count;
        public uint NameRaw;
        public uint Null2;
        public short Index;
        public string Variable;

        public override string ToString()
        {
            return $"{Variable}: {Count} {Type}";
        }
    }
}