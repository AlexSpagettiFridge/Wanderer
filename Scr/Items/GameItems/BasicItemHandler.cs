using Godot;

namespace Wanderer.Items.GameItems
{
    internal class BasicItemHandler : ItemHandler
    {
        private readonly string name;
        private readonly bool isSmall;
        private readonly Texture2D texture;

        public override string Name => name;
        public override bool IsSmall => isSmall;

        public BasicItemHandler(string name, Texture2D texture, bool isSmall = false)
        {
            this.name = name;
            this.texture = texture;
            this.isSmall = isSmall;
        }

        public override Texture2D GetTexture(Item item) => texture;
    }
}