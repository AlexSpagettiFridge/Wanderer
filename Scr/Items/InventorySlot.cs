using System;
namespace Wanderer.Items
{
    internal class InventorySlot
    {
        private Item[] items = new Item[1];
        private Predicate<Item> requirements = null;

        public InventorySlot(Predicate<Item> requirements = null)
        {
            this.requirements = requirements;
        }

        public Item[] Items
        {
            get => items;
        }

        public bool TryInsertItem(Item item)
        {
            if (requirements != null)
            {
                if (!requirements.Invoke(item)) { return false; }
            }
            bool isItemSmall = item.GetHandler().IsSmall;
            if (IsQuadSlot && !isItemSmall) { return false; }

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] != null) { continue; }
                if (isItemSmall) { IsQuadSlot = true; }
                items[i] = item;
                ItemChanged?.Invoke(this, items);
                return true;
            }

            return false;
        }

        public void RemoveItem(Item item)
        {
            bool isEmpty = true;
            for (int i = 0; i < Items.Length; i++)
            {
                if (items[i] != null)
                {
                    if (items[i] == item)
                    {
                        items[i] = null;
                        continue;
                    }
                }
                isEmpty = false;
            }
            if (isEmpty)
            {
                IsQuadSlot = true;
            }
            ItemChanged?.Invoke(this, items);
        }

        public bool IsQuadSlot
        {
            get => Items.LongLength == 4;
            set
            {
                if (value == IsQuadSlot) { return; }
                if (value)
                {
                    items = new Item[4];
                }
                else
                {
                    items = new Item[1];
                }
                for (int i = 0; i < items.Length; i++)
                {
                    items[0] = null;
                }
            }
        }

        public delegate bool CheckItem(Item item);
        public event EventHandler<Item[]> ItemChanged;
    }
}