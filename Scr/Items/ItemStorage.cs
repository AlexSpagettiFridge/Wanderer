using System.Collections.Generic;
using System.Linq;

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

        public int Size
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

        public bool GiveItem(Item item)
        {
            InventorySlot freeSlot = inventory.Find((x)=>x.Item==null);
            if (freeSlot!=null)
            {
                freeSlot.Item = item;
            }
            return false;
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