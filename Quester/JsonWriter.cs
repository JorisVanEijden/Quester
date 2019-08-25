using System;
using System.Collections.Generic;
using System.IO;

namespace Quester
{
    internal static class JsonWriter
    {
        private static void WriteStates(TextWriter writer, IReadOnlyDictionary<short, State> states)
        {
            writer.WriteLine($"\"states: [{states.Count}]\": {{");
            for (short i = 0; i < states.Count; i++)
            {
                State state = states[i];
                writer.WriteLine($"\"{i,2}: {state}\": {{");
                writer.WriteLine($"\"variable\": \"{state.Variable}\",");
                writer.WriteLine($"\"isGlobal\": {state.IsGlobal.ToString().ToLower()},");
                writer.WriteLine($"\"globalIndex\": {state.GlobalIndex},");
                writer.WriteLine($"\"index\": {state.Index}");
                writer.WriteLine(i + 1 == states.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteMobs(TextWriter writer, IReadOnlyDictionary<short, Mob> mobs)
        {
            writer.WriteLine($"\"mobs: [{mobs.Count}]\": {{");
            for (short i = 0; i < mobs.Count; i++)
            {
                Mob mob = mobs[i];
                writer.WriteLine($"\"{i,2}: {mob}\": {{");
                writer.WriteLine($"\"variable\": \"{mob.Variable}\",");
                writer.WriteLine($"\"type\": \"{mob.Type}\",");
                writer.WriteLine($"\"count\": {mob.Count},");
                writer.WriteLine($"\"null1\": {mob.Null1},");
                writer.WriteLine($"\"null2\": {mob.Null2},");
                writer.WriteLine($"\"index\": {mob.Index}");
                writer.WriteLine(i + 1 == mobs.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteTimers(TextWriter writer, IReadOnlyDictionary<short, Timer> timers)
        {
            writer.WriteLine($"\"timers: [{timers.Count}]\": {{");
            for (short i = 0; i < timers.Count; i++)
            {
                Timer timer = timers[i];
                writer.WriteLine($"\"{i,2}: {timer}\": {{");
                writer.WriteLine($"\"variable\": \"{timer.Variable}\",");
                writer.WriteLine($"\"type\": \"{timer.Type}\",");
                TimeSpan minimum = TimeSpan.FromMinutes(timer.Minimum);
                writer.WriteLine($"\"minimum\": \"{minimum}\",");
                TimeSpan maximum = TimeSpan.FromMinutes(timer.Maximum);
                writer.WriteLine($"\"maximum\": \"{maximum}\",");
                writer.WriteLine($"\"link1\": {timer.Link1},");
                writer.WriteLine($"\"link2\": {timer.Link2},");
                writer.WriteLine($"\"duration\": {timer.Duration},");
                writer.WriteLine($"\"started\": {timer.Started},");
                writer.WriteLine($"\"flags\": {timer.Flags},");
                writer.WriteLine($"\"index\": {timer.Index}");
                writer.WriteLine(i + 1 == timers.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteLocations(TextWriter writer, IReadOnlyDictionary<short, Location> locations)
        {
            writer.WriteLine($"\"locations: [{locations.Count}]\": {{");
            for (short i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];
                writer.WriteLine($"\"{i,2}: {location}\": {{");
                writer.WriteLine($"\"variable\": \"{location.Variable}\",");
                writer.WriteLine($"\"generalLocation\": \"{location.GeneralLocation}\",");
                writer.WriteLine($"\"locationType\": {location.LocationTypeRaw},");
                writer.WriteLine($"\"LocationType\": \"{location.LocationType}\",");
                writer.WriteLine($"\"fineLocation\": {location.FineLocation},");
                writer.WriteLine($"\"knownLocation\": \"{location.KnownLocation}\",");
                writer.WriteLine($"\"doorSelector\": {location.DoorSelector},");
                writer.WriteLine($"\"textRecordId1\": {location.TextRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {location.TextRecordId2},");
                writer.WriteLine($"\"index\": {location.Index},");
                writer.WriteLine($"\"flags\": {location.Flags},");
                writer.WriteLine($"\"objPtr\": {location.ObjPtr},");
                writer.WriteLine($"\"unknown1\": {location.Unknown1}");
                writer.WriteLine(i + 1 == locations.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteNpcs(TextWriter writer, IReadOnlyDictionary<short, Npc> npcs)
        {
            writer.WriteLine($"\"npcs: [{npcs.Count}]\": {{");
            for (short i = 0; i < npcs.Count; i++)
            {
                Npc npc = npcs[i];
                writer.WriteLine($"\"{i,2}: {npc}\": {{");
                writer.WriteLine($"\"variable\": \"{npc.Variable}\",");
                writer.WriteLine($"\"gender\": \"{npc.Gender}\",");
                writer.WriteLine($"\"facePictureIndex\": {npc.FacePictureIndex},");
                writer.WriteLine($"\"factionType\": {npc.FactionType},");
                writer.WriteLine($"\"faction\": \"{npc.Faction}\",");
                writer.WriteLine($"\"factionRaw\": {npc.FactionRaw},");
                writer.WriteLine($"\"textRecordId1\": {npc.TextRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {npc.TextRecordId2},");
                writer.WriteLine($"\"index\": {npc.Index},");
                writer.WriteLine($"\"unknown1\": {npc.Unknown1}");
                writer.WriteLine(i + 1 == npcs.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteItems(TextWriter writer, IReadOnlyDictionary<short, Item> items)
        {
            writer.WriteLine($"\"items: [{items.Count}]\": {{");
            for (short i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                writer.WriteLine($"\"{i,2}: {item}\": {{");
                writer.WriteLine($"\"variable\": \"{item.Variable}\",");
                writer.WriteLine($"\"rewardType\": \"{item.RewardType}\",");
                writer.WriteLine($"\"category\": \"{item.Category}\",");
                writer.WriteLine($"\"itemId\": {item.ItemId},");
                writer.WriteLine($"\"textRecordId1\": {item.TextRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {item.TextRecordId2},");
                writer.WriteLine($"\"index\": {item.Index},");
                writer.WriteLine($"\"unknown1\": {item.Unknown1}");
                writer.WriteLine(i + 1 == items.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        private static void WriteOpCodes(TextWriter writer, IReadOnlyList<OpCode> opCodes)
        {
            writer.WriteLine($"\"opCodes: [{opCodes.Count}]\": {{");
            for (var j = 0; j < opCodes.Count; j++)
            {
                OpCode opCode = opCodes[j];
                writer.WriteLine($"\"{j,2}: {opCode}\": {{");
                writer.WriteLine($"\"opCode\": {(int) opCode.Code},");
                writer.WriteLine($"\"argCount\": {opCode.ArgCount},");
                writer.WriteLine("\"arguments:\": {");
                for (var i = 0; i < opCode.Arguments.Count; i++)
                {
                    Argument argument = opCode.Arguments[i];
                    writer.WriteLine($"\"{i}: {argument}\": {{");
                    writer.WriteLine($"\"type\": \"{argument.Type}\",");
                    writer.WriteLine($"\"value\": \"{argument.Value}\",");
                    writer.WriteLine($"\"not\": \"{argument.Not}\",");
                    writer.WriteLine($"\"index\": \"{argument.Index}\",");
                    writer.WriteLine($"\"unknown1\": \"{argument.Unknown1}\"");
                    writer.WriteLine(i + 1 == opCode.Arguments.Count ? "}" : "},");
                }

                writer.WriteLine("},");
                writer.WriteLine($"\"messageId\": {opCode.MessageId},");
                writer.WriteLine($"\"flags\": {opCode.Flags},");
                writer.WriteLine($"\"lastUpdate\": {opCode.LastUpdate}");
                writer.WriteLine(j + 1 == opCodes.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void Write(TextWriter writer, Quest quest)
        {
            writer.WriteLine("{");
            WriteItems(writer, quest.Items);
            writer.Write(",");
            WriteNpcs(writer, quest.Npcs);
            writer.Write(",");
            WriteLocations(writer, quest.Locations);
            writer.Write(",");
            WriteTimers(writer, quest.Timers);
            writer.Write(",");
            WriteMobs(writer, quest.Mobs);
            writer.Write(",");
            WriteOpCodes(writer, quest.OpCodes);
            writer.Write(",");
            WriteStates(writer, quest.States);
            writer.WriteLine("}");
        }
    }
}