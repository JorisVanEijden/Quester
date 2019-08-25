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
        public string Name;
    }
}