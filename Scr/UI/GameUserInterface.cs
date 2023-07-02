using Godot;
using System;
using Wanderer.Info;
using Wanderer.Ui.Gui;

namespace Wanderer.Ui
{
    internal partial class GameUserInterface : Control
    {
        [Export]
        private HeroResourceBar staminaBar;
        [Export]
        private PackedScene psInventoryWindow = null, psSkillWindow;
        private Control currentWindow = null;

        public override void _Ready()
        {
            staminaBar.PresentedResource = Util.GameData.HeroData.Stamina;
        }

        public override void _Input(InputEvent @event)
        {
            if (@event.IsActionPressed("ui_inventory"))
            {
                if (currentWindow == null)
                {
                    currentWindow = psInventoryWindow.Instantiate<Control>();
                    AddChild(currentWindow);
                }
                else
                {
                    currentWindow.QueueFree();
                    currentWindow = null;
                }
            }
            if (@event.IsActionPressed("ui_skillmenu"))
            {
                if (currentWindow == null)
                {
                    currentWindow = psSkillWindow.Instantiate<Control>();
                    AddChild(currentWindow);
                }
                else
                {
                    currentWindow.QueueFree();
                    currentWindow = null;
                }
            }
        }
    }
}