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
    }
}