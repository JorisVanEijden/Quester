using System;
using System.Collections.Generic;
using System.Text;

namespace Quester
{
    internal class OpCode
    {
        public short ArgCount;
        public List<Argument> Arguments;
        public Instruction Code;
        public short Flags;
        public uint LastUpdate;
        public short MessageId;

        public override string ToString()
        {
            return GetCodeLine();
        }

        private string GetCodeLine()
        {
            Argument stateArgument = Arguments[0];
            stateArgument.Type = RecordType.State;
            string state = stateArgument.Value < 0 ? string.Empty : stateArgument.ToString();
            Arguments[0] = stateArgument;
            string message = (MessageId > 0) ? $" [Msg {MessageId}]" : "";

            string line;
            switch (Code)
            {
                case Instruction.DisplayMessage:
                    return $"{state} => Say ({MessageId})";
                case Instruction.WhenItemIsUsed:
                case Instruction.WhenPlayerCasts:
                case Instruction.WhenAtLocation:
                    return $"{state} >> {Code} ({Arguments[2]}): set {Arguments[1]}{message}";
                case Instruction.WhenPlayerHasItems:
                    line = $"{state} >> {Code} (";
                    for (var i = 2; i < ArgCount; i++)
                    {
                        line += Arguments[i].ToString();
                        if (i < ArgCount - 1 && Arguments[i + 1].ToString().Length >= 1)
                            line += ", ";
                    }

                    return line + $"): set {Arguments[1]}{message}";
                case Instruction.TriggerOnAndStates:
                    line = " >> When (";
                    for (var i = 1; i < ArgCount; i++)
                    {
                        line += Arguments[i].ToString();
                        if (i < ArgCount - 1 && Arguments[i + 1].ToString().Length >= 1)
                            line += " and ";
                    }

                    return line + $"): set {state}{message}";
                case Instruction.TriggerOnOrStates:
                    line = $"{state} >> When (";
                    for (var i = 2; i < ArgCount; i++)
                    {
                        line += Arguments[i].ToString();
                        if (i < ArgCount - 1 && Arguments[i + 1].ToString().Length >= 1)
                            line += " or ";
                    }

                    return line + $"): set {Arguments[1]}{message}";
                case Instruction.WhenTimeOfDayBetween:
                    TimeSpan start = new TimeSpan(0, Arguments[1].Value, 0);
                    TimeSpan end = new TimeSpan(0, Arguments[2].Value, 0);
                    return $" >> {Code} ({start}, {end}): set {state}{message}";
                case Instruction.WhenGivingItemToNpc:
                    return $" >> {Code} ({Arguments[1]}, {Arguments[2]}): set {state}{message}";
                case Instruction.StartTimer:
                    return $" >> WhenTimerExpires ({Arguments[1]}): set s_{Arguments[1].Value}{message}";
                case Instruction.CreateFoe:
                    Argument mobArgument = Arguments[1];
                    mobArgument.Type = RecordType.Mob;
                    Arguments[1] = mobArgument;
                    return $"{state} => CreateFoe({mobArgument}, {Arguments[2]}, {Arguments[3]}%, {Arguments[3]}){message}";
                default:
                    StringBuilder sb = new StringBuilder(state);
                    sb.Append($" => {Code} (");
                    for (var i = 1; i < ArgCount; i++)
                    {
                        var argument = Arguments[i].ToString();
                        sb.Append(argument);
                        if (argument.Length > 0 && i < ArgCount - 1 && Arguments[i + 1].ToString().Length > 0)
                            sb.Append(", ");
                    }

                    sb.Append(")");
                    sb.Append(message);
                    return sb.ToString();
            }
        }
    }
}