using System.Collections.Generic;
using Wanderer.Items;

namespace Wanderer.Info
{
    internal class HeroData
    {
        private int hp;
        private List<InventorySlot> inventory = new List<InventorySlot>();
        private List<Item> droppedItems;

        public int Hp { get => hp; set => hp = value; }
        public int InventorySize
        {
            get => inventory.Count;
            set
            {
                int originalCount = inventory.Count;
                if (value > originalCount)
                {
                    for (int index = originalCount - 1; index < value; index++)
                    {
                        inventory.Add(new InventorySlot());
                    }
                }
                else
                {
                    for (int index = originalCount - 1; index > value - 1; index--)
                    {
                        Item droppedSlotItem = inventory[index - 1].Item;
                        if (droppedSlotItem != null)
                        {
                            droppedItems.Add(droppedSlotItem);
                        }
                        inventory.Remove(inventory[index - 1]);
                    }
                }
            }
        }

        internal HeroData()
        {
            InventorySize = 12;
        }

        public List<InventorySlot> GetInventorySlots() => inventory;
        public List<Item> FlushDroppedItems()
        {
            List<Item> result = droppedItems;
            droppedItems = new List<Item>();
            return result;
        }
    }
}