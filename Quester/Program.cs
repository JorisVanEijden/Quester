using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLine;
using CommandLine.Text;
using static Quester.Reader;

namespace Quester
{
    internal static class Program
    {
        internal static Quest Quest;

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
        }

        private static void Run(Options options)
        {
            foreach (string file in options.FileNames)
            {
                if (!File.Exists(file))
                {
                    Console.Error.WriteLine("Failed to open {0}", file);
                    continue;
                }

                var name = Path.GetFileNameWithoutExtension(file);
                Quest = new Quest {name = name };
                ParseFile(file);
                name = @"../../../docs/" + name;
                OutputJson(name);
            }
        }

        private static void OutputJson(string name)
        {
            TextWriter writer = new StringWriter();
            WriteJson(writer);

            var jsonFile = $"{name}.json";
            File.Delete(jsonFile);
            File.WriteAllText(jsonFile, writer.ToString());
        }

        private static void WriteJson(TextWriter writer)
        {
            writer.WriteLine("{");
            JsonWriter.WriteItems(writer, Quest.items);
            writer.Write(",");
            JsonWriter.WriteNpcs(writer, Quest.npcs);
            writer.Write(",");
            JsonWriter.WriteLocations(writer, Quest.locations);
            writer.Write(",");
            JsonWriter.WriteTimers(writer, Quest.timers);
            writer.Write(",");
            JsonWriter.WriteMobs(writer, Quest.mobs);
            writer.Write(",");
            JsonWriter.WriteOpcodes(writer, Quest.opCodes);
            writer.Write(",");
            JsonWriter.WriteStates(writer, Quest.states);
            writer.WriteLine("}");
        }

        private static void ParseFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                QbnHeader header = ReadHeader(reader);
                Quest.items = ReadItemSection(reader, header);
                Quest.npcs = ReadNpcSection(reader, header);
                Quest.locations = ReadLocationSection(reader, header);
                Quest.timers = ReadTimerSection(reader, header);
                Quest.mobs = ReadMobSection(reader, header);
                Quest.opCodes = ReadOpCodeSection(reader, header);
                Quest.states = ReadStateSection(reader, header);
            }
        }

        private class Options
        {
            [Value(0, MetaName = "fileName", Required = true, HelpText = "One or more paths to QBN files.")]
            public IEnumerable<string> FileNames { get; private set; }

            [Usage(ApplicationAlias = "Quester")]
            public static IEnumerable<Example> Examples =>
                new List<Example>
                {
                    new Example("Decompile QBN files.", new Options { FileNames = new[] { @"Daggerfall\Arena2\S0000011.QBN" } })
                };
        }
    }

    internal struct Quest
    {
        public Dictionary<short, Item> items;
        public Dictionary<short, Npc> npcs;
        public Dictionary<short, Location> locations;
        public Dictionary<short, Timer> timers;
        public Dictionary<short, Mob> mobs;
        public List<OpCode> opCodes;
        public Dictionary<short, State> states;
        public string name;
    }

    internal class OpCode
    {
        public short argCount;
        public List<Argument> arguments;
        public Instruction code;
        public short flags;
        public uint lastUpdate;
        public short messageId;

        public override string ToString()
        {
            return GetCodeLine();
        }

        private string GetCodeLine()
        {
            Argument stateArgument = arguments[0];
            stateArgument.type = RecordType.State;
            string state = stateArgument.value < 0 ? string.Empty : stateArgument.ToString();
            arguments[0] = stateArgument;
            string line;
            switch (code)
            {
                case Instruction.DisplayMessage:
                    return $"{state} => Say ({messageId})";
                case Instruction.WhenItemIsUsed:
                case Instruction.WhenPlayerCasts:
                case Instruction.WhenAtLocation:
                    return $"{state} >> {code} ({arguments[2]}): set {arguments[1]}";
                case Instruction.WhenPlayerHasItems:
                    line = $"{state} >> {code} (";
                    for (var i = 2; i < argCount; i++)
                    {
                        line += arguments[i].ToString();
                        if (i < argCount - 1 && arguments[i + 1].ToString().Length >= 1)
                            line += ", ";
                    }

                    return line + $"): set {arguments[1]}";
                case Instruction.TriggerOnAndStates:
                    line = " >> When (";
                    for (var i = 1; i < argCount; i++)
                    {
                        line += arguments[i].ToString();
                        if (i < argCount - 1 && arguments[i + 1].ToString().Length >= 1)
                            line += " and ";
                    }

                    return line + $"): set {state}";
                case Instruction.TriggerOnOrStates:
                    line = $"{state} >> When (";
                    for (var i = 2; i < argCount; i++)
                    {
                        line += arguments[i].ToString();
                        if (i < argCount - 1 && arguments[i + 1].ToString().Length >= 1)
                            line += " or ";
                    }

                    return line + $"): set {arguments[1]}";
                case Instruction.WhenTimeOfDayBetween:
                    TimeSpan start = new TimeSpan(0, arguments[1].value, 0);
                    TimeSpan end = new TimeSpan(0, arguments[2].value, 0);
                    return $" >> {code} ({start}, {end}): set {state}";
                case Instruction.WhenGivingItemToNpc:
                    return $" >> {code} ({arguments[1]}, {arguments[2]}): set {state}";
                case Instruction.StartTimer:
                    return $" >> WhenTimerExpires ({arguments[1]}): set s_{arguments[1].value}";
                case Instruction.CreateFoe:
                    Argument mobArgument = arguments[1];
                    mobArgument.type = RecordType.Mob;
                    arguments[1] = mobArgument;
                    return $"{state} => CreateFoe({mobArgument}, {arguments[2]}, {arguments[3]}%, {arguments[3]})";
            }


            StringBuilder sb = new StringBuilder(state);
            sb.Append($" => {code} (");
            for (var i = 1; i < argCount; i++)
            {
                var argument = arguments[i].ToString();
                sb.Append(argument);
                if (argument.Length > 0 && i < argCount - 1 && arguments[i + 1].ToString().Length > 0)
                    sb.Append(", ");
            }

            sb.Append(")");

            if (messageId > 0)
                sb.Append($" [Msg {messageId}]");

            return sb.ToString();
        }
    }


    internal struct Argument
    {
        public bool not;
        public uint index;
        public RecordType type;
        public int value;
        public int unknown1;

        public override string ToString()
        {
            // Override special cases.
            switch (type)
            {
                case RecordType.State when value == -2:
                case RecordType.Item when value == -1:
                case RecordType.Location when value == -1:
                case RecordType.Npc when value == -1:
                    return string.Empty;
                case RecordType.State when value == -1:
                    return "_";
            }

            var variable = string.Empty;
            try
            {
                switch (type)
                {
                    case RecordType.Item:
                        variable = Program.Quest.items[(short) value].variable;
                        break;
                    case RecordType.Location:
                        variable = Program.Quest.locations[(short) value].variable;
                        break;
                    case RecordType.Mob:
                        variable = Program.Quest.mobs[(short) value].variable;
                        break;
                    case RecordType.Npc:
                        variable = Program.Quest.npcs[(short) value].variable;
                        break;
                    case RecordType.State:
                        variable = "s_" + Program.Quest.states[(short) value].index;
                        break;
                    case RecordType.Timer:
                        variable = Program.Quest.timers[(short) value].variable;
                        break;
                    case RecordType.Text:
                        variable = $"\"{value}\"";
                        break;
                    case RecordType.Value:
                        variable = $"{value}";
                        break;
                    default:
                        variable = $"ERR:{type}={value}";
                        Console.Error.WriteLine($"{Program.Quest.name}: ERROR: {type}={value}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{Program.Quest.name}: ERROR: {type}={value}");
                throw;
            }

            return (not ? "! " : "") + variable;
        }
    }

    internal struct State
    {
        public bool isGlobal;
        public byte globalIndex;
        public string variable;
        public short index;

        public override string ToString()
        {
            return isGlobal ? variable.ToUpper() : variable;
        }
    }

    internal struct Mob
    {
        public ushort null1;
        public MobType type;
        public ushort count;
        public string variable;
        public uint null2;
        public short index;

        public override string ToString()
        {
            return $"{variable}: {count} {type}";
        }
    }

    internal struct Timer
    {
        public ushort flags;
        public TimerType type;
        public int minimum;
        public int maximum;
        public uint started;
        public uint duration;
        public int link1;
        public int link2;
        public string variable;
        public short index;

        public override string ToString()
        {
            return $"{variable}: {type}";
        }
    }

    internal struct Location
    {
        public byte flags;
        public GeneralLocation generalLocation;
        public ushort fineLocation;
        public LocationType LocationType;
        public short doorSelector;
        public ushort unknown1;
        public string variable;
        public uint objPtr;
        public ushort textRecordId1;
        public ushort textRecordId2;
        public short index;
        public short locationType;
        public NamedPlace knownLocation;

        public override string ToString()
        {
            var name = LocationType == LocationType.SpecificLocation ? knownLocation.ToString() : LocationType.ToString();
            return $"{variable}: {generalLocation} {name}";
        }
    }

    internal struct Npc
    {
        public Gender gender;
        public byte facePictureIndex;
        public ushort factionType;
        public FactionId faction;
        public ushort factionRaw;
        public string variable;
        public ushort textRecordId1;
        public ushort textRecordId2;
        public uint unknown1;
        public short index;

        public override string ToString()
        {
            var factionName = faction == 0 ? "" : faction.ToString();
            return $"{variable}: {gender} {factionName}";
        }
    }

    internal struct Item
    {
        public RewardType rewardType;
        public ItemCategory category;
        public ushort itemId;
        public string variable;
        public ushort textRecordId1;
        public ushort textRecordId2;
        public uint unknown1;
        public short index;

        public override string ToString()
        {
            if (rewardType == RewardType.Gold || rewardType == RewardType.Unknown)
            {
                return (category == ItemCategory.Random) ? $"Random gold" : $"{itemId} - {category} gold";
            }

            if (!ItemMapper.ItemMap.ContainsKey(category))
            {
                Console.Error.WriteLine("{0}: Failed to find item category {1} in item map", Program.Quest.name, category);
                return "ERROR";
            }

            string name;
            try
            {
                name = itemId == 0xffff ? category.ToString() : ItemMapper.ItemMap[category][itemId];
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{Program.Quest.name}: no {itemId} in {category}");
                throw;
            }

            var item = $"{variable}: {rewardType} {category} {name}";
            if (textRecordId1 > 0)
                item += $" [{textRecordId1}]";
            if (textRecordId2 > 0)
                item += $" [{textRecordId2}]";
            return item;
        }
    }

    internal struct QbnHeader
    {
        public ushort questId;
        public ushort factionId;
        public ushort resourceId;
        public char[] resourceFilename;
        public byte hasDebugInfo;
        public ushort itemsSectionCount;
        public ushort npcsSectionCount;
        public ushort locationsSectionCount;
        public ushort timersSectionCount;
        public ushort mobsSectionCount;
        public ushort opCodesSectionCount;
        public ushort statesSectionCount;
        public ushort itemsSectionOffset;
        public ushort npcsSectionOffset;
        public ushort locationsSectionOffset;
        public ushort timersSectionOffset;
        public ushort mobsSectionOffset;
        public ushort opCodesSectionOffset;
        public ushort statesSectionOffset;
        public ushort section1count;
        public ushort section2count;
        public ushort section5count;
        public ushort section1offset;
        public ushort section2offset;
        public ushort section5offset;
        public ushort textVariableOffset;
        public ushort null2;
    }
}