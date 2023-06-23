using System;
using System.Collections.Generic;

namespace Wanderer.Items
{
    internal class ItemStorage
    {
        private List<InventorySlot> inventory = new List<InventorySlot>();
        private List<Item> droppedItems = new List<Item>();

        public ItemStorage(int size)
        {
            Size = size;
        }

        public ItemStorage(int size, List<Predicate<Item>> requirements)
        {
            foreach (Predicate<Item> requirement in requirements)
            {
                inventory.Add(new InventorySlot(requirement));
            }
            Size = size;
        }

        public int Size
        {
            get => inventory.Count;
            set
            {
                int originalCount = inventory.Count;
                if (value > originalCount)
                {
                    for (int index = originalCount - 1; index < value - 1; index++)
                    {
                        inventory.Add(new InventorySlot());
                    }
                }
                else
                {
                    for (int index = originalCount - 1; index > value - 1; index--)
                    {
                        foreach (Item droppedSlotItem in inventory[index - 1].Items)
                        {
                            if (droppedSlotItem != null)
                            {
                                droppedItems.Add(droppedSlotItem);
                            }
                        }
                        inventory.Remove(inventory[index - 1]);
                    }
                }
            }
        }

        public bool GiveItem(Item item)
        {
            foreach (InventorySlot slot in inventory)
            {
                if (slot.TryInsertItem(item) == InventorySlot.TryInsertResult.Success)
                {
                    return true;
                }
            }
            return false;
        }

        public List<InventorySlot> GetInventorySlots() => inventory;

        public InventorySlot this[int x]
        {
            get => inventory[x];
            set => inventory[x] = value;
        }

        public List<Item> FlushDroppedItems()
        {
            List<Item> result = droppedItems;
            droppedItems = new List<Item>();
            return result;
        }
    }
}