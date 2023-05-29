using Godot;
using System;

namespace Wanderer.UI
{
    [Tool]
    internal partial class Inventory : GridContainer
    {
        private Vector2 themeSeperators = new Vector2();

        public override void _Ready()
        {
            Resized += OnResize;
            OnResize();

        }

        public void InstanceInventoryItems(int itemCount)
        {
            if (!Engine.IsEditorHint())
            {
                for (int itemIndex = 0; itemIndex < itemCount; itemIndex++)
                {
                    InventoryItem ii = new InventoryItem();
                    AddChild(ii);
                }
            }
        }

        public override void _Draw()
        {
            if (Engine.IsEditorHint())
            {
                for (int x = 0; x < Columns; x++)
                {
                    for (int y = 0; y < Math.Floor(Size.Y / (64 + Theme.GetConstant("v_seperator", "GridContainer"))) / 64; y++)
                    {
                        DrawRect(new Rect2(x * 64, y * 64, 64, 64), new Color(1, 1, 1, 1), false, 2);
                    }
                }
            }
        }

        private void OnResize()
        {
            Columns = (int)Math.Max(1 + Math.Floor((Size.X - 64) / (64 + themeSeperators.X)), 1);
        }

        private void OnThemeChanged()
        {
            Theme inheritedTheme = Util.GetInheritedTheme(this);
            themeSeperators.X = inheritedTheme.GetConstant("h_seperator", "GridContainer");
            themeSeperators.Y = inheritedTheme.GetConstant("v_seperator", "GridContainer");
        }
    }


}