using System;

namespace Wanderer.Items
{
    internal class InventorySlot
    {
        private Item item = null;

        public Item Item
        {
            get => item;
            set
            {
                item = value;
                ItemChanged?.Invoke(this, item);
            }

        }
        public delegate bool CheckItem(Item item);
        public event EventHandler<Item> ItemChanged;
    }
}