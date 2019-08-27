using System.Collections.Generic;

namespace Quester
{
    internal struct Quest
    {
        public Dictionary<short, Item> Items;
        public Dictionary<short, Npc> Npcs;
        public Dictionary<short, Location> Locations;
        public Dictionary<short, Timer> Timers;
        public Dictionary<short, Mob> Mobs;
        public List<OpCode> OpCodes;
        public Dictionary<short, State> States;

        private static readonly Dictionary<char, string> QuestTypes = new Dictionary<char, string>
        {
            ['_'] = "Starting",
            ['$'] = "Vampire/Lycanthropy",
            ['0'] = "Julianos",
            ['1'] = "Meridia",
            ['2'] = "Molag Bal",
            ['3'] = "Namira",
            ['4'] = "Nocturnal",
            ['5'] = "Peryite",
            ['6'] = "Sheogorath",
            ['7'] = "Sanguine",
            ['8'] = "Malacath",
            ['9'] = "Vaermina",
            ['A'] = "Common",
            ['B'] = "KnightS",
            ['C'] = "Temple",
            ['D'] = "Akatosh",
            ['E'] = "Arkay",
            ['F'] = "Dibella",
            ['G'] = "Kynara",
            ['H'] = "Mara",
            ['I'] = "Stendarr",
            ['J'] = "Zenithar",
            ['K'] = "Merchant",
            ['L'] = "Dark Brotherhood",
            ['M'] = "Fighters Guild",
            ['N'] = "Mage Guild",
            ['O'] = "Thieves Guild",
            ['P'] = "Vampire",
            ['Q'] = "Coven",
            ['R'] = "Royalty",
            ['S'] = "Main",
            ['T'] = "Azura",
            ['U'] = "Boethiah",
            ['V'] = "Clavicus Vile",
            ['W'] = "Hermaeus Mora",
            ['X'] = "Hircine",
            ['Y'] = "Mehrune Dagon",
            ['Z'] = "Mephala"
        };


        public Info Info;
        private string _name;
        public Dictionary<int, List<string>> TextRecords;

        public string Name
        {
            get => _name;
            set
            {
                _name = value.ToUpper();
                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            Info.Name = _name;
            Info.QuestType = QuestTypes[_name[0]];
            switch (_name[2])
            {
                case 'A':
                    Info.Membership = Membership.Prospect;
                    break;
                case 'B':
                    Info.Membership = Membership.Member;
                    break;
                case 'C':
                    Info.Membership = Membership.NonMember;
                    break;
                default:
                    Info.Membership = Membership.None;
                    break;
            }

            if (int.TryParse($"{_name[3]}", out var reputation))
                Info.Reputation = 10 * reputation;
            Info.ChildSafe = _name[4] == '0';
            switch (_name[5])
            {
                case 'Y':
                    Info.Delivery = Delivery.InPerson;
                    break;
                case 'L':
                    Info.Delivery = Delivery.ByLetter;
                    break;
                default:
                    Info.Delivery = Delivery.Unknown;
                    break;
            }
        }
    }

    internal enum Membership
    {
        Prospect,
        NonMember,
        Member,
        None
    }

    internal enum Delivery
    {
        InPerson,
        ByLetter,
        Unknown
    }
}