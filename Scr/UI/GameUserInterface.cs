using Godot;
using System;

namespace Wanderer.UI
{
    internal partial class GameUserInterface : Control
    {
        [Export]
        private PackedScene psInventoryWindow = null;
        private Control currentWindow = null;
        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("ui_inventory"))
            {
				if (currentWindow == null)
				{
					currentWindow = psInventoryWindow.Instantiate<Control>();
					AddChild(currentWindow);
				}else
				{
					currentWindow.QueueFree();
					currentWindow = null;
				}
            }
        }
    }
}