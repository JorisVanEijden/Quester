namespace Quester
{
    internal struct Location
    {
        public byte Flags;
        public GeneralLocation GeneralLocation;
        public ushort FineLocation;
        public LocationType LocationType;
        public short DoorSelector;
        public ushort Unknown1;
        public uint NameRaw;
        public uint ObjPtr;
        public ushort TextRecordId1;
        public ushort TextRecordId2;
        public short Index;
        public short LocationTypeRaw;
        public NamedPlace KnownLocation;
        public string Variable;

        public override string ToString()
        {
            var name = LocationType == LocationType.SpecificLocation ? KnownLocation.ToString() : LocationType.ToString();
            return $"{Variable}: {GeneralLocation} {name}";
        }
    }
}