namespace Quester
{
    internal struct Npc
    {
        public Gender Gender;
        public byte FacePictureIndex;
        public NpcType NpcType;
        public FactionId Faction;
        public ushort FactionRaw;
        public ushort NpcTypeRaw;
        public string Variable;
        public ushort TextRecordId1;
        public ushort TextRecordId2;
        public uint Null1;
        public short Index;

        public override string ToString()
        {
            string npc = $"{Variable}: {Gender}";
            if (NpcType < NpcType.Unknown_7)
            {
                npc += $" (type {NpcType})";
            }

            if (Faction != FactionId.None)
            {
                npc += $" (faction {Faction})";
            }

            if (FacePictureIndex > 0 && FacePictureIndex < 255)
            {
                npc += $" (face {FacePictureIndex})";
            }

            return npc;
        }
    }
}