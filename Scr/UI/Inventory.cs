using Godot;
using System;
using System.Collections.Generic;
using Wanderer.Items;

namespace Wanderer.Ui
{
    [Tool]
    internal partial class Inventory : GridContainer
    {
        private Vector2 themeSeparation = new Vector2();

        public override void _Ready()
        {
            Resized += OnResize;
            OnResize();
            OnThemeChanged();
        }

        public void InstanceInventoryItems(List<InventorySlot> inventorySlots)
        {
            if (!Engine.IsEditorHint())
            {
                for (int itemIndex = 0; itemIndex < inventorySlots.Count; itemIndex++)
                {
                    InventoryItem ii = new InventoryItem(inventorySlots[itemIndex]);
                    AddChild(ii);
                }
            }
        }

        public override void _Draw()
        {
            Vector2 size = new Vector2(64, 64);
            if (Engine.IsEditorHint())
            {
                for (float x = 64; x <= Size.X; x += 64 + themeSeparation.X)
                {
                    for (float y = 64; y <= Size.Y; y += 64 + themeSeparation.Y)
                    {
                        DrawRect(new Rect2(new Vector2(x, y) - size, size), new Color(1, 1, 1, 1), false, 2);
                    }
                }
            }
        }

        private void OnResize()
        {
            Columns = (int)Math.Max(1 + Math.Floor((Size.X - 64) / (64 + themeSeparation.X)), 1);
            QueueRedraw();
        }

        private void OnThemeChanged()
        {
            Theme inheritedTheme = Util.GetInheritedTheme(this);
            themeSeparation.X = inheritedTheme.GetConstant("h_separation", "GridContainer");
            themeSeparation.Y = inheritedTheme.GetConstant("v_separation", "GridContainer");
        }
    }


}