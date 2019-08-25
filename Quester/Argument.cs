using System;

namespace Quester
{
    internal struct Argument
    {
        public bool Not;
        public uint Index;
        public RecordType Type;
        public int Value;
        public int Unknown1;

        public override string ToString()
        {
            // Override special cases.
            switch (Type)
            {
                case RecordType.State when Value == -2:
                case RecordType.Item when Value == -1:
                case RecordType.Location when Value == -1:
                case RecordType.Npc when Value == -1:
                    return string.Empty;
                case RecordType.State when Value == -1:
                    return "_";
            }

            string variable  ;
            try
            {
                switch (Type)
                {
                    case RecordType.Item:
                        variable = Program.Quest.Items[(short) Value].Variable;
                        break;
                    case RecordType.Location:
                        variable = Program.Quest.Locations[(short) Value].Variable;
                        break;
                    case RecordType.Mob:
                        variable = Program.Quest.Mobs[(short) Value].Variable;
                        break;
                    case RecordType.Npc:
                        variable = Program.Quest.Npcs[(short) Value].Variable;
                        break;
                    case RecordType.State:
                        variable = "s_" + Program.Quest.States[(short) Value].Index;
                        break;
                    case RecordType.Timer:
                        variable = Program.Quest.Timers[(short) Value].Variable;
                        break;
                    case RecordType.Text:
                        variable = $"\"{Value}\"";
                        break;
                    case RecordType.Value:
                        variable = $"{Value}";
                        break;
                    default:
                        variable = $"ERR:{Type}={Value}";
                        Console.Error.WriteLine($"{Program.Quest.Name}: ERROR: {Type}={Value}");
                        break;
                }
            }
            catch (Exception )
            {
                Console.Error.WriteLine($"{Program.Quest.Name}: ERROR: {Type}={Value}");
                throw;
            }

            return (Not ? "! " : "") + variable;
        }
    }
}