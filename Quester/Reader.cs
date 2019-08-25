using System;
using System.Collections.Generic;
using System.IO;

namespace Quester
{
    internal static class Reader
    {
        public static QbnHeader ReadHeader(BinaryReader reader)
        {
            QbnHeader header = new QbnHeader
            {
                questId = reader.ReadUInt16(),
                factionId = reader.ReadUInt16(),
                resourceId = reader.ReadUInt16(),
                resourceFilename = reader.ReadChars(9),
                hasDebugInfo = reader.ReadByte(),
                itemsSectionCount = reader.ReadUInt16(),
                section1count = reader.ReadUInt16(),
                section2count = reader.ReadUInt16(),
                // Section records counts
                npcsSectionCount = reader.ReadUInt16(),
                locationsSectionCount = reader.ReadUInt16(),
                section5count = reader.ReadUInt16(),
                timersSectionCount = reader.ReadUInt16(),
                mobsSectionCount = reader.ReadUInt16(),
                opCodesSectionCount = reader.ReadUInt16(),
                statesSectionCount = reader.ReadUInt16(),
                // Section offsets
                itemsSectionOffset = reader.ReadUInt16(),
                section1offset = reader.ReadUInt16(),
                section2offset = reader.ReadUInt16(),
                npcsSectionOffset = reader.ReadUInt16(),
                locationsSectionOffset = reader.ReadUInt16(),
                section5offset = reader.ReadUInt16(),
                timersSectionOffset = reader.ReadUInt16(),
                mobsSectionOffset = reader.ReadUInt16(),
                opCodesSectionOffset = reader.ReadUInt16(),
                statesSectionOffset = reader.ReadUInt16(),
                textVariableOffset = reader.ReadUInt16(),
                null2 = reader.ReadUInt16()
            };

            return header;
        }

        public static Dictionary<short, Item> ReadItemSection(BinaryReader reader, QbnHeader header)
        {
            var items = new Dictionary<short, Item>();
            reader.BaseStream.Seek(header.itemsSectionOffset, 0);
            for (var i = 0; i < header.itemsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                items[index] = new Item
                {
                    index = index,
                    rewardType = (RewardType) reader.ReadByte(),
                    category = (ItemCategory) reader.ReadUInt16(),
                    itemId = reader.ReadUInt16(),
                    variable = "i_" + VariableNames.LookUp(reader.ReadUInt32()),
                    unknown1 = reader.ReadUInt32(),
                    textRecordId1 = reader.ReadUInt16(),
                    textRecordId2 = reader.ReadUInt16()
                };
            }

            return items;
        }

        public static Dictionary<short, Npc> ReadNpcSection(BinaryReader reader, QbnHeader header)
        {
            var npcs = new Dictionary<short, Npc>();
            reader.BaseStream.Seek(header.npcsSectionOffset, 0);
            for (var i = 0; i < header.npcsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                Npc npc = new Npc
                {
                    index = index,
                    gender = (Gender) reader.ReadByte(),
                    facePictureIndex = reader.ReadByte(),
                    factionType = reader.ReadUInt16(),
                    factionRaw = reader.ReadUInt16(),
                    variable = "n_" + VariableNames.LookUp(reader.ReadUInt32()),
                    unknown1 = reader.ReadUInt32(),
                    textRecordId1 = reader.ReadUInt16(),
                    textRecordId2 = reader.ReadUInt16()
                };
                npc.faction = (FactionId) npc.factionRaw;
                npcs[index] = npc;
            }

            return npcs;
        }

        public static Dictionary<short, Location> ReadLocationSection(BinaryReader reader, QbnHeader header)
        {
            var locations = new Dictionary<short, Location>();
            reader.BaseStream.Seek(header.locationsSectionOffset, 0);
            for (var i = 0; i < header.locationsSectionCount; i++)
            {
                var index = reader.ReadInt16();

                Location location = new Location
                {
                    index = index,
                    flags = reader.ReadByte(),
                    generalLocation = (GeneralLocation) reader.ReadByte(),
                    fineLocation = reader.ReadUInt16(),
                    locationType = reader.ReadInt16(),
                    doorSelector = reader.ReadInt16(),
                    unknown1 = reader.ReadUInt16(),
                    variable = "l_" + VariableNames.LookUp(reader.ReadUInt32()),
                    objPtr = reader.ReadUInt32(),
                    textRecordId1 = reader.ReadUInt16(),
                    textRecordId2 = reader.ReadUInt16()
                };

                if (location.generalLocation == GeneralLocation.Specific)
                {
                    location.LocationType = LocationType.SpecificLocation;
                    location.knownLocation = (NamedPlace) location.fineLocation;
                }
                else
                {
                    location.knownLocation = NamedPlace.None;
                    if (location.fineLocation == 0)
                        location.LocationType = (LocationType) location.locationType;
                    else
                        location.LocationType = (LocationType) location.locationType + 500;
                }

                locations[index] = location;
            }

            return locations;
        }

        public static Dictionary<short, Timer> ReadTimerSection(BinaryReader reader, QbnHeader header)
        {
            var timers = new Dictionary<short, Timer>();
            reader.BaseStream.Seek(header.timersSectionOffset, 0);
            for (var i = 0; i < header.timersSectionCount; i++)
            {
                var index = reader.ReadInt16();

                timers[index] = new Timer
                {
                    index = index,
                    flags = reader.ReadUInt16(),
                    type = (TimerType) reader.ReadByte(),
                    minimum = reader.ReadInt32(),
                    maximum = reader.ReadInt32(),
                    started = reader.ReadUInt32(),
                    duration = reader.ReadUInt32(),
                    link1 = reader.ReadInt32(),
                    link2 = reader.ReadInt32(),
                    variable = "t_" + VariableNames.LookUp(reader.ReadUInt32())
                };
            }

            return timers;
        }

        public static Dictionary<short, Mob> ReadMobSection(BinaryReader reader, QbnHeader header)
        {
            var mobs = new Dictionary<short, Mob>();
            reader.BaseStream.Seek(header.mobsSectionOffset, 0);
            for (var i = 0; i < header.mobsSectionCount; i++)
            {
                short index = reader.ReadByte();

                mobs[index] = new Mob
                {
                    index = index,
                    null1 = reader.ReadUInt16(),
                    type = (MobType) reader.ReadByte(),
                    count = reader.ReadUInt16(),
                    variable = "m_" + VariableNames.LookUp(reader.ReadUInt32()),
                    null2 = reader.ReadUInt32()
                };
            }

            return mobs;
        }

        public static List<OpCode> ReadOpCodeSection(BinaryReader reader, QbnHeader header)
        {
            var opCodes = new List<OpCode>();
            reader.BaseStream.Seek(header.opCodesSectionOffset, 0);
            for (var i = 0; i < header.opCodesSectionCount; i++)
            {
                OpCode opCode = new OpCode();
                opCode.code = (Instruction) reader.ReadInt16();
                opCode.flags = reader.ReadInt16();
                opCode.argCount = reader.ReadInt16();
                opCode.arguments = ReadArguments(reader, opCode.argCount);
                opCode.messageId = reader.ReadInt16();
                opCode.lastUpdate = reader.ReadUInt32();
                opCodes.Add(opCode);
            }

            return opCodes;
        }

        public static Dictionary<short, State> ReadStateSection(BinaryReader reader, QbnHeader header)
        {
            var states = new Dictionary<short, State>();
            reader.BaseStream.Seek(header.statesSectionOffset, 0);
            for (var i = 0; i < header.statesSectionCount; i++)
            {
                var index = reader.ReadInt16();

                states[index] = new State
                {
                    index = index,
                    isGlobal = reader.ReadBoolean(),
                    globalIndex = reader.ReadByte(),
                    variable = "s_" + VariableNames.LookUp(reader.ReadUInt32())
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
                    argument.not = reader.ReadBoolean();
                    argument.index = reader.ReadUInt32();
                    argument.type = (RecordType) reader.ReadInt16();
                    argument.value = reader.ReadInt32();
                    argument.unknown1 = reader.ReadInt32();
                }
                if (argument.index == 0x12345678 || argument.index == 0x1234567)
                {
                    argument.type = RecordType.Value;
                    argument.index = 0;
                }
                else
                {
                    argument.index &= 255;
                }

                if (arguments.Count < count)
                    arguments.Add(argument);
            }

            return arguments;
        }
    }
}