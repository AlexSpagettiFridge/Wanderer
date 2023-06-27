using System;
using System.Collections.Generic;

namespace Wanderer.Items
{
    internal class ItemStorage
    {
        private List<InventorySlot> inventory = new List<InventorySlot>();
        private List<Item> droppedItems = new List<Item>();
        public event EventHandler<InventorySlotChangedArgs> SlotItemChanged;

        public ItemStorage(int size)
        {
            Size = size;
        }

        public ItemStorage(int size, List<Predicate<Item>> requirements)
        {
            foreach (Predicate<Item> requirement in requirements)
            {
                Add(new InventorySlot(requirement));
            }
            Size = size;
        }

        /// <summary>
        /// Use this instead of inventory.Add(x). So events are automatically Updated.
        /// </summary>
        /// <param name="inventorySlot"></param>
        private void Add(InventorySlot inventorySlot)
        {
            inventorySlot.ItemChanged += (x, y) => SlotItemChanged?.Invoke(x, y);
            inventory.Add(inventorySlot);
        }

        /// <summary>
        /// Same as <see cref="Add"/> but for inventory.Remove(x).
        /// </summary>
        /// <param name="inventorySlot"></param>
        private void Remove(InventorySlot inventorySlot)
        {
            inventorySlot.ItemChanged -= (x, y) => SlotItemChanged?.Invoke(x, y);
            inventory.Remove(inventorySlot);
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
                        Add(new InventorySlot());
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
                        Remove(inventory[index - 1]);
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

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            foreach(InventorySlot slot in inventory)
            {
                foreach(Item item in slot.Items)
                {
                    if (item!=null)
                    {
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        public List<Item> FlushDroppedItems()
        {
            List<Item> result = droppedItems;
            droppedItems = new List<Item>();
            return result;
        }
    }
}