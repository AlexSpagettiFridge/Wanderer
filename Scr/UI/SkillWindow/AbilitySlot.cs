using System.Text.Json;
using Godot;
using Wanderer.Abilities;

namespace Wanderer.Ui.SkillWindow
{
    [Tool]
    internal partial class AbilitySlot : AbilityCardBase
    {
        [Export]
        private int index = 0;

        public override void _Ready()
        {
            shownAbility = Util.GameData.HeroData.Abilities[index];
        }

        public override void _GuiInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton inputEventMouseButton)
            {
                if (inputEventMouseButton.Pressed && inputEventMouseButton.ButtonIndex == MouseButton.Right)
                {
                    Util.GameData.HeroData.Abilities[index] = null;
                    shownAbility = null;
                }
            }
        }

        public override bool _CanDropData(Vector2 atPosition, Variant data)
        {
            JsonElement root = JsonDocument.Parse((string)data).RootElement;
            if (!root.TryGetProperty("DataType", out JsonElement dataTypeElement)) { return false; }
            return dataTypeElement.ToString() == "Item";
        }

        public override void _DropData(Vector2 atPosition, Variant data)
        {
            JsonElement root = JsonDocument.Parse((string)data).RootElement;
            if (!root.TryGetProperty("Ability", out JsonElement abilityElement)) { return; }
            Ability ability = Ability.CreateFromJson(abilityElement);
            Util.GameData.HeroData.Abilities[index] = ability;
            shownAbility = ability;
        }

    }
}