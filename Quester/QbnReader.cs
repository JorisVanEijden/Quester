using System;
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

                var item = new Item
                {
                    Index = index,
                    RewardType = (RewardType)reader.ReadByte(),
                    Category = (ItemCategory)reader.ReadUInt16(),
                    ItemId = reader.ReadUInt16(),
                    NameRaw = reader.ReadUInt32(),
                    Unknown1 = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };
                var name = VariableNames.LookUp(item.NameRaw) ?? $"{index:D2}";
                item.Variable = "i_" + name;
                items[index] = item;
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
                    Unknown1 = reader.ReadByte(),
                    FacePictureIndex = reader.ReadByte(),
                    NpcTypeRaw = reader.ReadUInt16(),
                    FactionRaw = reader.ReadUInt16(),
                    NameRaw = reader.ReadUInt32(),
                    Null1 = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };
                npc.NpcType = (NpcType)npc.NpcTypeRaw;
                npc.Faction = (FactionId)npc.FactionRaw;
                var name = VariableNames.LookUp(npc.NameRaw) ?? $"{index:D2}";
                npc.Variable = "n_" + name;
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
                    GeneralLocation = (GeneralLocation)reader.ReadByte(),
                    FineLocation = reader.ReadUInt16(),
                    LocationTypeRaw = reader.ReadInt16(),
                    DoorSelector = reader.ReadInt16(),
                    Unknown1 = reader.ReadUInt16(),
                    NameRaw = reader.ReadUInt32(),
                    ObjPtr = reader.ReadUInt32(),
                    TextRecordId1 = reader.ReadUInt16(),
                    TextRecordId2 = reader.ReadUInt16()
                };

                if (location.GeneralLocation == GeneralLocation.Specific)
                {
                    location.LocationType = LocationType.SpecificLocation;
                    location.KnownLocation = (NamedPlace)location.FineLocation;
                }
                else
                {
                    location.KnownLocation = NamedPlace.None;
                    if (location.FineLocation == 0)
                        location.LocationType = (LocationType)location.LocationTypeRaw;
                    else
                        location.LocationType = (LocationType)location.LocationTypeRaw + 500;
                }
                var name = VariableNames.LookUp(location.NameRaw) ?? $"{index:D2}";
                location.Variable = "l_" + name;

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

                Timer timer = new Timer
                {
                    Index = index,
                    Flags1 = reader.ReadByte(),
                    Flags2 = reader.ReadByte(),
                    TypeRaw = reader.ReadByte(),
                    Minimum = reader.ReadInt32(),
                    Maximum = reader.ReadInt32(),
                    Started = reader.ReadUInt32(),
                    Duration = reader.ReadUInt32(),
                    Link1 = reader.ReadInt32(),
                    Link2 = reader.ReadInt32(),
                    NameRaw = reader.ReadUInt32()
                };
                timer.Type = (TimerType)timer.TypeRaw;
                timer.Link1Type = ((timer.Flags2 & 0x1) == 0) ? RecordType.Location : RecordType.Npc;
                timer.Link2Type = ((timer.Flags2 & 0x2) == 0) ? RecordType.Location : RecordType.Npc;
                var name = VariableNames.LookUp(timer.NameRaw) ?? $"{index:D2}";
                timer.Variable = "t_" + name;

                timers[index] = timer;

//                TimeSpan minimum = TimeSpan.FromMinutes(timer.Minimum);
//                TimeSpan maximum = TimeSpan.FromMinutes(timer.Maximum);

//                string binary1 = Convert.ToString(timer.Flags1, 2).PadLeft(8, '0');
//                Console.WriteLine($"{Program.Quest.Name} {Program.Quest.Locations.Count} {Program.Quest.Npcs.Count} {timer.Type} '{binary1}' {timer.Link1Type} {timer.Link1} {timer.Link2Type} {timer.Link2} {minimum} {maximum}");
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

                var mob = new Mob
                {
                    Index = index,
                    Null1 = reader.ReadUInt16(),
                    Type = (MobType)reader.ReadByte(),
                    Count = reader.ReadUInt16(),
                    NameRaw = reader.ReadUInt32(),
                    Null2 = reader.ReadUInt32()
                };
                var name = VariableNames.LookUp(mob.NameRaw) ?? $"{index:D2}";
                mob.Variable = "m_" + name;

                mobs[index] = mob;
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
                opCode.Code = (Instruction)reader.ReadInt16();
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
            for (int i = 0; i < header.StatesSectionCount; i++)
            {
                short index = reader.ReadInt16();

                var state = new State
                {
                    Index = index,
                    IsGlobal = reader.ReadBoolean(),
                    GlobalIndex = reader.ReadByte(),
                    NameRaw = reader.ReadUInt32()
                };

                if (state.IsGlobal)
                {
                    if (Enum.IsDefined(typeof(GlobalStates), (int)state.GlobalIndex))
                        state.Variable = "gs_" + (GlobalStates)state.GlobalIndex;
                    else
                        state.Variable = $"gs_{state.GlobalIndex:D2}";
                }
                else
                {
                    var name = VariableNames.LookUp(state.NameRaw) ?? $"{index:D2}";
                    state.Variable = "s_" + name;
                }

                states[index] = state;
            }

            return states;
        }

        private static List<Argument> ReadArguments(BinaryReader reader, int count)
        {
            var arguments = new List<Argument>();
            for (int i = 0; i < 5; i++)
            {
                Argument argument = new Argument();
                {
                    argument.Not = reader.ReadBoolean();
                    argument.Index = reader.ReadUInt32();
                    argument.Type = (RecordType)reader.ReadInt16();
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