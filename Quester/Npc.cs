namespace Quester
{
    internal struct Npc
    {
        public Gender Gender;
        public byte FacePictureIndex;
        public ushort FactionType;
        public FactionId Faction;
        public ushort FactionRaw;
        public string Variable;
        public ushort TextRecordId1;
        public ushort TextRecordId2;
        public uint Unknown1;
        public short Index;

        public override string ToString()
        {
            var factionName = Faction == 0 ? "" : Faction.ToString();
            return $"{Variable}: {Gender} {factionName}";
        }
    }
}