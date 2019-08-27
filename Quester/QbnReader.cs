using System.Collections.Generic;
using System.IO;

namespace Quester
{
    internal static class QbnReader
    {
        public static QbnHeader ReadHeader(BinaryReader reader)
        {
            QbnHeader header = new QbnHeader
            {
                QuestId = reader.ReadUInt16(),
                FactionId = reader.ReadUInt16(),
                ResourceId = reader.ReadUInt16(),
                ResourceFilename = reader.ReadChars(9),
                HasDebugInfo = reader.ReadByte(),
                ItemsSectionCount = reader.ReadUInt16(),
                Section1Count = reader.ReadUInt16(),
                Section2Count = reader.ReadUInt16(),
                // Section records counts
                NpcsSectionCount = reader.ReadUInt16(),
                LocationsSectionCount = reader.ReadUInt16(),
                Section5Count = reader.ReadUInt16(),
                TimersSectionCount = reader.ReadUInt16(),
                MobsSectionCount = reader.ReadUInt16(),
                OpCodesSectionCount = reader.ReadUInt16(),
                StatesSectionCount = reader.ReadUInt16(),
                // Section offsets
                ItemsSectionOffset = reader.ReadUInt16(),
                Section1Offset = reader.ReadUInt16(),
                Section2Offset = reader.ReadUInt16(),
                NpcsSectionOffset = reader.ReadUInt16(),
                LocationsSectionOffset = reader.ReadUInt16(),
                Section5Offset = reader.ReadUInt16(),
                TimersSectionOffset = reader.ReadUInt16(),
                MobsSectionOffset = reader.ReadUInt16(),
                OpCodesSectionOffset = reader.ReadUInt16(),
                StatesSectionOffset = reader.ReadUInt16(),
                TextVariableOffset = reader.ReadUInt16(),
                Null2 = reader.ReadUInt16()
            };

            return header;
        }

        public static Dictionary<short, Item> ReadItemSection(BinaryReader reader, QbnHeader header)
        {
            var items = new Dictionary<short, Item>();
            reader.BaseStream.Seek(header.ItemsSectionOffset, 0);
            for (var i = 0; i < header.ItemsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                items[index] = new Item
                {
                    Index = index,
                    RewardType = (RewardType) reader.ReadByte(),
                    Category = (ItemCategory) reader.ReadUInt16(),
                    ItemId = reader.ReadUInt16(),
                    Variable = "i_" + VariableNames.LookUp(reader.ReadUInt32()),
                    Unknown1 = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };
            }

            return items;
        }

        public static Dictionary<short, Npc> ReadNpcSection(BinaryReader reader, QbnHeader header)
        {
            var npcs = new Dictionary<short, Npc>();
            reader.BaseStream.Seek(header.NpcsSectionOffset, 0);
            for (var i = 0; i < header.NpcsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                Npc npc = new Npc
                {
                    Index = index,
                    Gender = (Gender) reader.ReadByte(),
                    FacePictureIndex = reader.ReadByte(),
                    NpcTypeRaw = reader.ReadUInt16(),
                    FactionRaw = reader.ReadUInt16(),
                    Variable = "n_" + VariableNames.LookUp(reader.ReadUInt32()),
                    Null1 = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };
                npc.NpcType = (NpcType) npc.NpcTypeRaw;
                npc.Faction = (FactionId) npc.FactionRaw;
                npcs[index] = npc;
            }

            return npcs;
        }

        public static Dictionary<short, Location> ReadLocationSection(BinaryReader reader, QbnHeader header)
        {
            var locations = new Dictionary<short, Location>();
            reader.BaseStream.Seek(header.LocationsSectionOffset, 0);
            for (var i = 0; i < header.LocationsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                Location location = new Location
                {
                    Index = index,
                    Flags = reader.ReadByte(),
                    GeneralLocation = (GeneralLocation) reader.ReadByte(),
                    FineLocation = reader.ReadUInt16(),
                    LocationTypeRaw = reader.ReadInt16(),
                    DoorSelector = reader.ReadInt16(),
                    Unknown1 = reader.ReadUInt16(),
                    Variable = "l_" + VariableNames.LookUp(reader.ReadUInt32()),
                    ObjPtr = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };

                if (location.GeneralLocation == GeneralLocation.Specific)
                {
                    location.LocationType = LocationType.SpecificLocation;
                    location.KnownLocation = (NamedPlace) location.FineLocation;
                }
                else
                {
                    location.KnownLocation = NamedPlace.None;
                    if (location.FineLocation == 0)
                        location.LocationType = (LocationType) location.LocationTypeRaw;
                    else
                        location.LocationType = (LocationType) location.LocationTypeRaw + 500;
                }

                locations[index] = location;
            }

            return locations;
        }

        public static Dictionary<short, Timer> ReadTimerSection(BinaryReader reader, QbnHeader header)
        {
            var timers = new Dictionary<short, Timer>();
            reader.BaseStream.Seek(header.TimersSectionOffset, 0);
            for (var i = 0; i < header.TimersSectionCount; i++)
            {
                var index = reader.ReadInt16();

                timers[index] = new Timer
                {
                    Index = index,
                    Flags = reader.ReadUInt16(),
                    Type = (TimerType) reader.ReadByte(),
                    Minimum = reader.ReadInt32(),
                    Maximum = reader.ReadInt32(),
                    Started = reader.ReadUInt32(),
                    Duration = reader.ReadUInt32(),
                    Link1 = reader.ReadInt32(),
                    Link2 = reader.ReadInt32(),
                    Variable = "t_" + VariableNames.LookUp(reader.ReadUInt32())
                };
            }

            return timers;
        }

        public static Dictionary<short, Mob> ReadMobSection(BinaryReader reader, QbnHeader header)
        {
            var mobs = new Dictionary<short, Mob>();
            reader.BaseStream.Seek(header.MobsSectionOffset, 0);
            for (var i = 0; i < header.MobsSectionCount; i++)
            {
                short index = reader.ReadByte();

                mobs[index] = new Mob
                {
                    Index = index,
                    Null1 = reader.ReadUInt16(),
                    Type = (MobType) reader.ReadByte(),
                    Count = reader.ReadUInt16(),
                    Variable = "m_" + VariableNames.LookUp(reader.ReadUInt32()),
                    Null2 = reader.ReadUInt32()
                };
            }

            return mobs;
        }

        public static List<OpCode> ReadOpCodeSection(BinaryReader reader, QbnHeader header)
        {
            var opCodes = new List<OpCode>();
            reader.BaseStream.Seek(header.OpCodesSectionOffset, 0);
            for (var i = 0; i < header.OpCodesSectionCount; i++)
            {
                // ReSharper disable once UseObjectOrCollectionInitializer
                OpCode opCode = new OpCode();
                opCode.Code = (Instruction) reader.ReadInt16();
                opCode.Flags = reader.ReadInt16();
                opCode.ArgCount = reader.ReadInt16();
                opCode.Arguments = ReadArguments(reader, opCode.ArgCount);
                opCode.MessageId = reader.ReadInt16();
                opCode.LastUpdate = reader.ReadUInt32();
                opCodes.Add(opCode);
            }

            return opCodes;
        }

        public static Dictionary<short, State> ReadStateSection(BinaryReader reader, QbnHeader header)
        {
            var states = new Dictionary<short, State>();
            reader.BaseStream.Seek(header.StatesSectionOffset, 0);
            for (var i = 0; i < header.StatesSectionCount; i++)
            {
                var index = reader.ReadInt16();

                states[index] = new State
                {
                    Index = index,
                    IsGlobal = reader.ReadBoolean(),
                    GlobalIndex = reader.ReadByte(),
                    Variable = "s_" + VariableNames.LookUp(reader.ReadUInt32())
                };
            }

            return states;
        }

        private static List<Argument> ReadArguments(BinaryReader reader, int count)
        {
            var arguments = new List<Argument>();
            for (var i = 0; i < 5; i++)
            {
                Argument argument = new Argument();
                {
                    argument.Not = reader.ReadBoolean();
                    argument.Index = reader.ReadUInt32();
                    argument.Type = (RecordType) reader.ReadInt16();
                    argument.Value = reader.ReadInt32();
                    argument.Unknown1 = reader.ReadInt32();
                }
                if (argument.Index == 0x12345678 || argument.Index == 0x1234567)
                {
                    argument.Type = RecordType.Value;
                    argument.Index = 0;
                }
                else
                {
                    argument.Index &= 255;
                }

                if (arguments.Count < count)
                    arguments.Add(argument);
            }

            return arguments;
        }
    }
}