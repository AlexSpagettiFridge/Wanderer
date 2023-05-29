using Godot;
using System;

namespace Wanderer.UI
{
    internal partial class PlayerInventoryWindow : Control
    {
        [Export]
        private Inventory playerInventory = null;
        public override void _Ready()
        {
            playerInventory.InstanceInventoryItems(20);
        }
    }
}