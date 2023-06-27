using System.Collections.Generic;
using Godot;
using Wanderer.Abilities;

namespace Wanderer.Ui.SkillWindow
{
    internal partial class SkillWindow : Control
    {
        [Export]
        private Control skillCardScroller;

        public override void _Ready()
        {
            InitSkillCardList();
        }

        private void InitSkillCardList()
        {
            foreach (KeyValuePair<string, List<Ability>> pair in Util.GameData.HeroData.GetAbilitiesByCategory())
            {
                AddCategory(pair.Key, pair.Value);
            }
        }

        private void AddCategory(string name, List<Ability> abilities)
        {
            Label label = new Label();
            label.Text = name;
            HBoxContainer hBox = new HBoxContainer();
            foreach (Ability ability in abilities)
            {
                AbilityCard card = new AbilityCard(ability);
                hBox.AddChild(card);
            }

            skillCardScroller.AddChild(label);
            skillCardScroller.AddChild(hBox);
        }
    }
}