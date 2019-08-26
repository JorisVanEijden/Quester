using System;

namespace Quester
{
    internal struct Item
    {
        public RewardType RewardType;
        public ItemCategory Category;
        public ushort ItemId;
        public string Variable;
        public ushort TextRecordId1;
        public ushort TextRecordId2;
        public uint Unknown1;
        public short Index;

        public override string ToString()
        {
            if (!ItemMapper.ItemMap.ContainsKey(Category))
            {
                Console.Error.WriteLine("{0}: Failed to find item category {1} in item map", Program.Quest.Name, Category);
                return "ERROR";
            }

            string item = $"{Variable}: ";
            if (RewardType == RewardType.Gold || RewardType == RewardType.Unknown)
            {
                item += (Category == ItemCategory.Random) ? "Random gold" : $"{ItemId} - {Category} gold";
            }
            else
            {
                try
                {
                    var name = ItemId == 0xffff ? Category.ToString() : ItemMapper.ItemMap[Category][ItemId];
                    item += $"{RewardType} {Category} {name}";
                }
                catch (Exception )
                {
                    Console.Error.WriteLine($"{Program.Quest.Name}: no {ItemId} in {Category}");
                    throw;
                }
            }

            if (TextRecordId1 > 0)
                item += $" [{TextRecordId1}]";
            if (TextRecordId2 > 0)
                item += $" [{TextRecordId2}]";

            return item;
        }
    }
}