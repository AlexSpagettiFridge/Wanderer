namespace Wanderer.Items
{
    internal class InventorySlot
    {
        private Item item = null;
        public delegate bool CheckItem(Item item);
    }
}