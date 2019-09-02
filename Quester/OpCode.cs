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
//                    return $"{state} >> {Code} ({Arguments[2]}, {Arguments[3]}): set {Arguments[1]}{message}";
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
                    line = " >> If (";
                    for (var i = 1; i < ArgCount; i++)
                    {
                        line += Arguments[i].ToString();
                        if (i < ArgCount - 1 && Arguments[i + 1].ToString().Length >= 1)
                            line += " and ";
                    }

                    return line + $"): set {state}{message}";
                case Instruction.TriggerOnOrStates:
                    line = $" >> If (";
                    for (var i = 1; i < ArgCount; i++)
                    {
                        line += Arguments[i].ToString();
                        if (i < ArgCount - 1 && Arguments[i + 1].ToString().Length >= 1)
                            line += " or ";
                    }

                    return line + $"): set {state}{message}";
                case Instruction.IfMobHurtByPlayer:
                case Instruction.IfItemPickedUp:
                case Instruction.IfNpcClicked:
                case Instruction.IfPlayerHasLevel:
                    return $" >> {Code} ({Arguments[1]}): set {state}{message}";
                case Instruction.IfTimeOfDayBetween:
                    TimeSpan start = new TimeSpan(0, Arguments[1].Value, 0);
                    TimeSpan end = new TimeSpan(0, Arguments[2].Value, 0);
                    return $" >> {Code} ({start}, {end}): set {state}{message}";
                case Instruction.IfGivingItemToNpc:
                case Instruction.IfMobsKilled:
                case Instruction.IfNpcReputation:
                case Instruction.IfItemDroppedAt:
                    return $" >> {Code} ({Arguments[1]}, {Arguments[2]}): set {state}{message}";
                case Instruction.IfFactionReputation:
                    FactionId faction = (FactionId) Arguments[1].Value;
                    return $" >> {Code} ({faction}, {Arguments[2]}): set {state}{message}";
                case Instruction.StartTimer:
                    State timerState = FindTimerState(Arguments[1]);
                    return $"{state} >> {Code} ({Arguments[1]}); When it expires: set {timerState}{message}";
                case Instruction.CreateFoe:
                    Argument mobArgument = Arguments[1];
                    mobArgument.Type = RecordType.Mob;
                    Arguments[1] = mobArgument;
                    return $"{state} => CreateFoe({mobArgument}, {Arguments[2]}, {Arguments[3]}%, {Arguments[4]}){message}";
                case Instruction.TeleportPlayer:
                    return $"{state} >> {Code} ({Arguments[1]}, {(Region)Arguments[2].Value}, 0x{Arguments[3].Value:X4}){message}";
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

        private State FindTimerState(Argument argument)
        {
            if (argument.Type != RecordType.Timer)
            {
                throw new ArgumentException($"Argument must be timer, not {argument.Type}");
            }

            Timer timer = Program.Quest.Timers[(short) argument.Index];
            string variable = timer.Variable.Substring(2);
            foreach (var state in Program.Quest.States)
            {
                if (state.Value.Variable.Substring(2) == variable)
                {
                    return state.Value;
                }
            }

            throw new Exception("State not found");
        }
    }
}