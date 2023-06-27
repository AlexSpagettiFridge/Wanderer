using System;

namespace Wanderer.Items
{
    internal class InventorySlotChangedArgs : EventArgs
    {
        public ChangeType Change;
        public Item Item;

        public InventorySlotChangedArgs(ChangeType change, Item item)
        {
            Change = change;
            Item = item;
        }

        public enum ChangeType { Added, Removed }
    }
}