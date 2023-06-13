using Godot;
using System;
using Wanderer.Items;

namespace Wanderer.UI
{
    internal partial class InventoryItem : Control
    {
        private static SpriteFrames frameSprite = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Frame.tres");
        private readonly Vector2 margin = new Vector2(4, 4);
        private InventorySlot inventorySlot = null;

        public InventoryItem() { }

        public InventoryItem(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            inventorySlot.ItemChanged += OnItemChanged;
        }

        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(64, 64);
            Draw += _Draw;
        }

        private void OnItemChanged(object sender,Item item)
        {
            QueueRedraw();
        }
        public override void _Draw()
        {
            DrawTextureRect(frameSprite.GetFrameTexture("mono", 0), new Rect2(Vector2.Zero, Size), false);
            if (inventorySlot == null) { return; }
            if (inventorySlot.Item == null) { return; }
            Texture2D itemTexture = inventorySlot.Item.GetItemHandler().GetTexture();
            DrawTextureRect(itemTexture, new Rect2(new Vector2(2, 2), Size - new Vector2(4, 4)), false);
        }
    }
}