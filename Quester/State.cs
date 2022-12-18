namespace Quester
{
    internal struct State
    {
        public bool IsGlobal;
        public byte GlobalIndex;
        public string Variable;
        public short Index;
        public uint NameRaw;

        public override string ToString()
        {
            return Variable;
        }
    }
}