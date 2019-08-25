namespace Quester
{
    internal struct Timer
    {
        public ushort Flags;
        public TimerType Type;
        public int Minimum;
        public int Maximum;
        public uint Started;
        public uint Duration;
        public int Link1;
        public int Link2;
        public string Variable;
        public short Index;

        public override string ToString()
        {
            return $"{Variable}: {Type}";
        }
    }
}