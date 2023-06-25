using Godot;

namespace Wanderer.Items
{
    /// <summary>
    /// Defines Item behaviour
    /// </summary>
    internal abstract class ItemHandler
    {
        /// <summary>
        /// The technical Name of this Handler. Must be unique as this is the main way to identify Handlers.
        /// </summary>
        /// <value></value>
        public abstract string Name { get; }
        /// <summary>
        /// The visible name for this Item.
        /// </summary>
        /// <returns></returns>
        public virtual string GetVisualName(Item item) => Util.Tr(Name);
        /// <summary>
        /// Wheter this item can share a InventorySlot with 3 other small items or not.
        /// </summary>
        public virtual bool IsSmall => false;
        /// <summary>
        /// The visual Icon for this Item
        /// </summary>
        /// <returns></returns>
        public abstract Texture2D GetTexture(Item item);
        public Item CreateItem()
        {
            return new Item(this);
        }
    }
}