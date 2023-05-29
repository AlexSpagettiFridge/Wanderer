using Godot;
using System;

namespace Wanderer.UI
{
    internal partial class InventoryItem : Control
    {
        public override void _Ready()
        {
            CustomMinimumSize = new Vector2(64, 64);
            Draw+=_Draw;
        }

        public override void _Draw()
        {
            DrawRect(new Rect2(Vector2.Zero,Size),new Color(1,1,1,1));
        }
    }
}