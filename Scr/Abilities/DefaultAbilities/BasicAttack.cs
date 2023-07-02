using Godot;
using Wanderer.Abilities.Costs;
using Wanderer.Entities;
using Wanderer.Entities.Projectiles;

namespace Wanderer.Abilities.DefaultAbilities
{
    internal class BasicAttack : AbilityHandler
    {
        public BasicAttack() : base("Attack") { }

        public override float GetCooldown(Ability ability) => 0.8f;

        public override AbilityCost GetCost(Ability ability) => new StaminaCost(42);

        public override void Invoke(Ability ability, Hero hero)
        {
            Util.TimerService.AddTimer(0.3f).Timeout += () =>
            {
                hero.AddChild(new SwordSwoosh(Mathf.DegToRad(45), hero.GetAimDirection().Angle(), 10, 30));
            };
        }

        public override Texture2D GetIconTexture(Ability ability) => defaultIcons.GetFrameTexture("Icons", 0);
    }
}