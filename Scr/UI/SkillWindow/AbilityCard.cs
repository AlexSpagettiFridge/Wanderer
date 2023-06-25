using Godot;
using Wanderer.Abilities;

namespace Wanderer.Ui.SkillWindow
{
    internal partial class AbilityCard : AbilityCardBase
    {
        public AbilityCard(Ability presentedAbility)
        {
            shownAbility = presentedAbility;
        }
    }
}