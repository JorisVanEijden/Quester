namespace Quester
{
    internal struct Npc
    {
        public byte Unknown1;
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
            // string npc = $"{Variable}: {Gender}";  Gender is not correct. but what is it?
            string npc = $"{Variable}: ";
            if (NpcType < NpcType.Normal)
            {
                npc += $" (type? {NpcType})";
            }

            if (Faction != FactionId.None)
            {
                npc += $" (faction {Faction})";
            }

            // Useless unless we can connect it to the actual pictures
            // if (FacePictureIndex > 0 && FacePictureIndex < 255)
            // {
            //     npc += $" (face? {FacePictureIndex})";
            // }

            return npc;
        }
    }
}