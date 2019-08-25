using System;
using System.Collections.Generic;
using System.IO;

namespace Quester
{
    internal static class JsonWriter
    {
        public static void WriteStates(TextWriter writer, Dictionary<short, State> states)
        {
            writer.WriteLine($"\"states: [{states.Count}]\": {{");
            for (short i = 0; i < states.Count; i++)
            {
                State state = states[i];
                writer.WriteLine($"\"{i,2}: {state}\": {{");
                writer.WriteLine($"\"variable\": \"{state.variable}\",");
                writer.WriteLine($"\"isGlobal\": {state.isGlobal.ToString().ToLower()},");
                writer.WriteLine($"\"globalIndex\": {state.globalIndex},");
                writer.WriteLine($"\"index\": {state.index}");
                writer.WriteLine(i + 1 == states.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteMobs(TextWriter writer, Dictionary<short, Mob> mobs)
        {
            writer.WriteLine($"\"mobs: [{mobs.Count}]\": {{");
            for (short i = 0; i < mobs.Count; i++)
            {
                Mob mob = mobs[i];
                writer.WriteLine($"\"{i,2}: {mob}\": {{");
                writer.WriteLine($"\"variable\": \"{mob.variable}\",");
                writer.WriteLine($"\"type\": \"{mob.type}\",");
                writer.WriteLine($"\"count\": {mob.count},");
                writer.WriteLine($"\"null1\": {mob.null1},");
                writer.WriteLine($"\"null2\": {mob.null2},");
                writer.WriteLine($"\"index\": {mob.index}");
                writer.WriteLine(i + 1 == mobs.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteTimers(TextWriter writer, Dictionary<short, Timer> timers)
        {
            writer.WriteLine($"\"timers: [{timers.Count}]\": {{");
            for (short i = 0; i < timers.Count; i++)
            {
                Timer timer = timers[i];
                writer.WriteLine($"\"{i,2}: {timer}\": {{");
                writer.WriteLine($"\"variable\": \"{timer.variable}\",");
                writer.WriteLine($"\"type\": \"{timer.type}\",");
                TimeSpan minimum = TimeSpan.FromMinutes(timer.minimum);
                writer.WriteLine($"\"minimum\": \"{minimum}\",");
                TimeSpan maximum = TimeSpan.FromMinutes(timer.maximum);
                writer.WriteLine($"\"maximum\": \"{maximum}\",");
                writer.WriteLine($"\"link1\": {timer.link1},");
                writer.WriteLine($"\"link2\": {timer.link2},");
                writer.WriteLine($"\"duration\": {timer.duration},");
                writer.WriteLine($"\"started\": {timer.started},");
                writer.WriteLine($"\"flags\": {timer.flags},");
                writer.WriteLine($"\"index\": {timer.index}");
                writer.WriteLine(i + 1 == timers.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteLocations(TextWriter writer, Dictionary<short, Location> locations)
        {
            writer.WriteLine($"\"locations: [{locations.Count}]\": {{");
            for (short i = 0; i < locations.Count; i++)
            {
                Location location = locations[i];
                writer.WriteLine($"\"{i,2}: {location}\": {{");
                writer.WriteLine($"\"variable\": \"{location.variable}\",");
                writer.WriteLine($"\"generalLocation\": \"{location.generalLocation}\",");
                writer.WriteLine($"\"locationType\": {location.locationType},");
                writer.WriteLine($"\"LocationType\": \"{location.LocationType}\",");
                writer.WriteLine($"\"fineLocation\": {location.fineLocation},");
                writer.WriteLine($"\"knownLocation\": \"{location.knownLocation}\",");
                writer.WriteLine($"\"doorSelector\": {location.doorSelector},");
                writer.WriteLine($"\"textRecordId1\": {location.textRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {location.textRecordId2},");
                writer.WriteLine($"\"index\": {location.index},");
                writer.WriteLine($"\"flags\": {location.flags},");
                writer.WriteLine($"\"objPtr\": {location.objPtr},");
                writer.WriteLine($"\"unknown1\": {location.unknown1}");
                writer.WriteLine(i + 1 == locations.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteNpcs(TextWriter writer, Dictionary<short, Npc> npcs)
        {
            writer.WriteLine($"\"npcs: [{npcs.Count}]\": {{");
            for (short i = 0; i < npcs.Count; i++)
            {
                Npc npc = npcs[i];
                writer.WriteLine($"\"{i,2}: {npc}\": {{");
                writer.WriteLine($"\"variable\": \"{npc.variable}\",");
                writer.WriteLine($"\"gender\": \"{npc.gender}\",");
                writer.WriteLine($"\"facePictureIndex\": {npc.facePictureIndex},");
                writer.WriteLine($"\"factionType\": {npc.factionType},");
                writer.WriteLine($"\"faction\": \"{npc.faction}\",");
                writer.WriteLine($"\"factionRaw\": {npc.factionRaw},");
                writer.WriteLine($"\"textRecordId1\": {npc.textRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {npc.textRecordId2},");
                writer.WriteLine($"\"index\": {npc.index},");
                writer.WriteLine($"\"unknown1\": {npc.unknown1}");
                writer.WriteLine(i + 1 == npcs.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteItems(TextWriter writer, Dictionary<short, Item> items)
        {
            writer.WriteLine($"\"items: [{items.Count}]\": {{");
            for (short i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                writer.WriteLine($"\"{i,2}: {item}\": {{");
                writer.WriteLine($"\"variable\": \"{item.variable}\",");
                writer.WriteLine($"\"rewardType\": \"{item.rewardType}\",");
                writer.WriteLine($"\"category\": \"{item.category}\",");
                writer.WriteLine($"\"itemId\": {item.itemId},");
                writer.WriteLine($"\"textRecordId1\": {item.textRecordId1},");
                writer.WriteLine($"\"textRecordId2\": {item.textRecordId2},");
                writer.WriteLine($"\"index\": {item.index},");
                writer.WriteLine($"\"unknown1\": {item.unknown1}");
                writer.WriteLine(i + 1 == items.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }

        public static void WriteOpcodes(TextWriter writer, List<OpCode> opCodes)
        {
            writer.WriteLine($"\"opCodes: [{opCodes.Count}]\": {{");
            for (var j = 0; j < opCodes.Count; j++)
            {
                OpCode opCode = opCodes[j];
                writer.WriteLine($"\"{j,2}: {opCode}\": {{");
                writer.WriteLine($"\"opCode\": {(int) opCode.code},");
                writer.WriteLine($"\"argCount\": {opCode.argCount},");
                writer.WriteLine("\"arguments:\": {");
                for (var i = 0; i < opCode.arguments.Count; i++)
                {
                    Argument argument = opCode.arguments[i];
                    writer.WriteLine($"\"{i}: {argument}\": {{");
                    writer.WriteLine($"\"type\": \"{argument.type}\",");
                    writer.WriteLine($"\"value\": \"{argument.value}\",");
                    writer.WriteLine($"\"not\": \"{argument.not}\",");
                    writer.WriteLine($"\"index\": \"{argument.index}\",");
                    writer.WriteLine($"\"unknown1\": \"{argument.unknown1}\"");
                    writer.WriteLine(i + 1 == opCode.arguments.Count ? "}" : "},");
                }

                writer.WriteLine("},");
                writer.WriteLine($"\"messageId\": {opCode.messageId}");
                writer.WriteLine(j + 1 == opCodes.Count ? "}" : "},");
            }

            writer.WriteLine("}");
        }
    }
}