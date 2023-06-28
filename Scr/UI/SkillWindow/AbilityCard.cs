using System.Text.Json;
using System.Text.Json.Nodes;
using Godot;
using Wanderer.Abilities;

namespace Wanderer.Ui.SkillWindow
{
    internal partial class AbilityCard : AbilityCardBase
    {
        internal AbilityCard() { }

        public AbilityCard(Ability presentedAbility)
        {
            shownAbility = presentedAbility;
        }

        public override Variant _GetDragData(Vector2 atPosition)
        {
            JsonNode jsonNode = JsonNode.Parse("{}");
            jsonNode["DataType"] = "Item";
            jsonNode["Ability"] = shownAbility.GetJson();
            return JsonSerializer.Serialize(jsonNode);;
        }
    }
}