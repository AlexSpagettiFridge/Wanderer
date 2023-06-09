namespace Wanderer.Items
{
    internal class InventorySlot
    {
        private Item item = null;

        public Item Item { get => item; set => item = value; }
        public delegate bool CheckItem(Item item);
    }
}