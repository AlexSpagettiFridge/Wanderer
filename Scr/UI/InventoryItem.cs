using Godot;
using System;

namespace Wanderer.UI
{
    internal partial class InventoryItem : Control
    {
        private static SpriteFrames frameSprite = ResourceLoader.Load<SpriteFrames>("res://Gfx/Items/Frame.tres");
        private readonly Vector2 margin = new Vector2(4, 4);
        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(64, 64);
            Draw += _Draw;
        }

        public override void _Draw()
        {
            DrawTextureRect(frameSprite.GetFrameTexture("mono", 0), new Rect2(Vector2.Zero,Size), false);
        }
    }
}