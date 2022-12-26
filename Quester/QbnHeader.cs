namespace Quester
{
    internal struct QbnHeader
    {
        public ushort QuestId;
        public ushort FactionId;
        public ushort ResourceId;
        public char[] ResourceFilename;
        public byte HasDebugInfo;
        public ushort ItemsSectionCount;
        public ushort NpcsSectionCount;
        public ushort LocationsSectionCount;
        public ushort TimersSectionCount;
        public ushort MobsSectionCount;
        public ushort OpCodesSectionCount;
        public ushort StatesSectionCount;
        public ushort ItemsSectionOffset;
        public ushort NpcsSectionOffset;
        public ushort LocationsSectionOffset;
        public ushort TimersSectionOffset;
        public ushort MobsSectionOffset;
        public ushort OpCodesSectionOffset;
        public ushort StatesSectionOffset;
        public ushort Section1Count;
        public ushort Section2Count;
        public ushort Section5Count;
        public ushort Section1Offset;
        public ushort Section2Offset;
        public ushort Section5Offset;
        public ushort TextVariableOffset;
        public ushort Null2;
        public Quest Quest { get; set; }
    }
}