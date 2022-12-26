namespace Quester
{
    internal struct Location
    {
        public byte Flags;
        public Locality Locality;
        public ushort LocationId;
        public LocationType LocationType;
        public short ExtraInfo2;
        public byte Unknown1;
        public byte Unknown2;
        public uint NameRaw;
        public uint ObjPtr;
        public ushort TextRecordId1;
        public ushort TextRecordId2;
        public short Index;
        public short ExtraInfo1;
        public string Variable;

        public override string ToString()
        {
            var display = $"{Variable}: {Locality} {LocationType}";
            
            if (ExtraInfo1 != -1 && (ExtraInfo1 & 0xfa00) == 0xfa00)
            {
                var marker = ExtraInfo1 & 0x00ff;
                display = $"{display} marker {marker}";
            }

            return display;
        }
    }
}