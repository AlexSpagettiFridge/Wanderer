using Gictionary = Godot.Collections.Dictionary;
namespace Wanderer.Items
{
    internal class Item
    {
        private ItemHandler itemHandler;
        private int id;
        public Gictionary SpecificData = new Gictionary();
        public ItemHandler GetItemHandler() => itemHandler;

        public Item(ItemHandler itemHandler)
        {
            this.itemHandler = itemHandler;
        }
        
    }
}