using Godot;
using Wanderer.Abilities.Costs;
using Wanderer.Entities;

namespace Wanderer.Abilities.DefaultAbilities
{
    internal class BasicAttack : AbilityHandler
    {
        public BasicAttack() : base("Attack") { }

        public override float GetCooldown(Ability ability) => 0.8f;

        public override AbilityCost GetCost(Ability ability) => new StaminaCost(42);

        public override void Invoke(Ability ability, Hero hero)
        {
            GD.Print("Moo");
        }

        public override Texture2D GetIconTexture(Ability ability) => defaultIcons.GetFrameTexture("Icons", 0);
    }
}