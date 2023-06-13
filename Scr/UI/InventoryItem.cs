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
        private string animation = "mono";

        public InventoryItem()
        {
            inventorySlot = new InventorySlot();
            inventorySlot.ItemChanged += OnItemChanged;
        }

        public InventoryItem(InventorySlot inventorySlot)
        {
            this.inventorySlot = inventorySlot;
            inventorySlot.ItemChanged += OnItemChanged;
            OnItemChanged(this, inventorySlot.Items);
        }

        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(64, 64);
            Draw += _Draw;
        }

        private void OnItemChanged(object sender, Item[] items)
        {
            animation = inventorySlot.IsQuadSlot ? "quad" : "mono";
            QueueRedraw();
        }
        public override void _Draw()
        {
            DrawTextureRect(frameSprite.GetFrameTexture(animation, 0), new Rect2(Vector2.Zero, Size), false);
            if (inventorySlot == null) { return; }
            if (inventorySlot.Items[0] == null) { return; }
            if (inventorySlot.IsQuadSlot)
            {
                Vector2 quadSize = Size / 2 - new Vector2(6, 6);
                Vector2[] texturePosition = new Vector2[] {
                    new Vector2(2, 2),
                    new Vector2(Size.X - quadSize.X-2, 2),
                    new Vector2(2,Size.Y - quadSize.Y-2),
                    Size-quadSize-new Vector2(2,2)
                };
                for (int i = 0; i < inventorySlot.Items.Length; i++)
                {
                    if (inventorySlot.Items[i] == null) { continue; }
                    Texture2D quadTexture = inventorySlot.Items[i].GetHandler().GetTexture();
                    DrawTextureRect(quadTexture, new Rect2(texturePosition[i], quadSize), false);
                }
                return;
            }
            Texture2D itemTexture = inventorySlot.Items[0].GetHandler().GetTexture();

            DrawTextureRect(itemTexture, new Rect2(new Vector2(2, 2), Size - new Vector2(4, 4)), false);
        }
    }
}