using Godot;
using System;
using Wanderer.Info;

namespace Wanderer.UI
{
    internal partial class PlayerInventoryWindow : Control
    {
        [Export]
        private Inventory playerInventory = null;
        public override void _Ready()
        {
            GameData gameData = GetNode<GameData>("/root/GameData");
            playerInventory.InstanceInventoryItems(gameData.HeroData.Storage.GetInventorySlots());
        }
    }
}