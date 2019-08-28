using System;

namespace Quester
{
    internal struct Timer
    {
        public TimerType Type;
        public int Minimum;
        public int Maximum;
        public uint Started;
        public uint Duration;
        public int Link1;
        public int Link2;
        public string Variable;
        public short Index;
        public byte TypeRaw;
        public byte Flags1;
        public byte Flags2;
        public RecordType Link1Type;
        public RecordType Link2Type;

        public override string ToString()
        {
            TimeSpan minimum = TimeSpan.FromMinutes(Minimum);
            TimeSpan maximum = TimeSpan.FromMinutes(Maximum);

            string location1;
            string location2;
            switch (Type)
            {
                case TimerType.Fixed:
                    return $"{Variable}: {Type} {minimum}";
                case TimerType.Random:
                    return $"{Variable}: {Type} between {minimum} and {maximum}";
                case TimerType.Relative1:
                case TimerType.Relative3:
                    location1 = Link1Type == RecordType.Location
                        ? Program.Quest.Locations[(short) Link1].Variable
                        : Program.Quest.Npcs[(short) Link1].Variable;
                    return $"{Variable}: 1.5 times travel time between here and '{location1}'";
                case TimerType.Relative2:
                    location1 = Link1Type == RecordType.Location
                        ? Program.Quest.Locations[(short) Link1].Variable
                        : Program.Quest.Npcs[(short) Link1].Variable;
                    location2 = Link2Type == RecordType.Location
                        ? Program.Quest.Locations[(short) Link2].Variable
                        : Program.Quest.Npcs[(short) Link2].Variable;
                    return $"{Variable}: 1.5 times travel time between '{location1}' and '{location2}'";
                case TimerType.Relative4:
                    location1 = Link1Type == RecordType.Location
                        ? Program.Quest.Locations[(short) Link1].Variable
                        : Program.Quest.Npcs[(short) Link1].Variable;
                    location2 = Link2Type == RecordType.Location
                        ? Program.Quest.Locations[(short) Link2].Variable
                        : Program.Quest.Npcs[(short) Link2].Variable;
                    return $"{Variable}: 1.5 times travel time from here to '{location1}' and then '{location2}'";
            }

            return $"{Variable}: {Type}";
        }
    }
}