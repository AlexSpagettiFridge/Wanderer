using Godot;

namespace Wanderer.Items
{
    /// <summary>
    /// Defines Item behaviour
    /// </summary>
    internal abstract class ItemHandler
    {
        private int id;

        internal int Id { get => id; set => id = value; }
        public abstract string Name { get; }
        public virtual bool IsSmall => false;
        public abstract Texture2D GetTexture();
        public Item CreateItem()
        {
            return new Item(this);
        }
    }
}