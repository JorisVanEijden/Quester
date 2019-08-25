using System;
using System.Collections.Generic;
using System.IO;
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
                Quest = new Quest
                {
                    Info = new Info(),
                    Name = name
                };
                ParseFile(file);
                name = @"../../../docs/" + name;
                try
                {
                    OutputJson(name);
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine($"ERROR in quest {Quest.Name}");
                    throw;
                }
            }
        }

        private static void OutputJson(string name)
        {
            var jsonFile = $"{name}.json";
            File.Delete(jsonFile);
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(jsonFile)))
            {
                JsonWriter.Write(writer, Quest);
            }
        }

        private static void ParseFile(string fileName)
        {
            if (!File.Exists(fileName)) throw new FileNotFoundException();

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                QbnHeader header = ReadHeader(reader);
                Quest.Items = ReadItemSection(reader, header);
                Quest.Npcs = ReadNpcSection(reader, header);
                Quest.Locations = ReadLocationSection(reader, header);
                Quest.Timers = ReadTimerSection(reader, header);
                Quest.Mobs = ReadMobSection(reader, header);
                Quest.OpCodes = ReadOpCodeSection(reader, header);
                Quest.States = ReadStateSection(reader, header);
                Quest.Info.QuestId = header.QuestId;
                Quest.Info.FactionId = (FactionId) header.FactionId;
                Quest.Info.ResourceId = header.ResourceId;
                Quest.Info.ResourceFileName = new string(header.ResourceFilename).Trim('\0');
                Quest.Info.HasDebugInfo = header.HasDebugInfo != 0 ? true : false;
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

    internal struct Info
    {
        public string Name;
        public string QuestType;
        public Membership Membership;
        public int Reputation;
        public bool ChildSafe;
        public Delivery Delivery;
        public ushort QuestId;
        public FactionId FactionId;
        public ushort ResourceId;
        public string ResourceFileName;
        public bool HasDebugInfo;
    }
}